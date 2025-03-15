using Godot;
using System;
using System.Collections.Generic;

public partial class AudioManager : Node
{
    // AUTOLOAD
    public static AudioManager Instance { get; private set; }

    // SETUP
    private int _3DStreamPlayerNum = 16;
    private bool _muteMusic = false;
    public bool MuteMusic
    {
        get { return _muteMusic; }
        set
        {
            _muteMusic = value;
            int musicBusIndex = AudioServer.GetBusIndex(SoundEffect.SoundType.MUSIC.ToString());
            AudioServer.SetBusMute(musicBusIndex, value);
        }
    }
    private float _replayMusicDelay = 15f;

    // OTHERS
    private Queue<AudioStreamPlayer3D> _avaiblePlayers;
    private AudioStreamPlayer3D _musicPlayer;
    private Timer _musicRestartTimer;
    private Dictionary<SoundEffect.SoundType, int> _soundSettings = new();


    public override void _EnterTree()
    {
        if (Instance != null)
        {
            this.QueueFree();
            return;
        }
        Instance = this;
    }
    public override void _Ready()
    {
        // Load settings
        LoadSoundSettings();

        // Set up background music
        _musicRestartTimer = new();
        AddChild(_musicRestartTimer);
        _musicRestartTimer.WaitTime = _replayMusicDelay;
        _musicRestartTimer.OneShot = true;
        _musicRestartTimer.Timeout += () => PlayMusic(ScenesController.Instance.currLevelController.LevelMusicAudioLibrary.GetRandomSound());

        // Set up sound queue
        _avaiblePlayers = new();

        for (int i = 0; i < _3DStreamPlayerNum; i++)
        {
            AudioStreamPlayer3D ap = new();
            AddChild(ap);
            _avaiblePlayers.Enqueue(ap);
            ap.Finished += () => { _OnStreamFinished(ap); };
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("mute"))
        {
            MuteMusic = !MuteMusic;
        }
    }

    private void _OnStreamFinished(AudioStreamPlayer3D p)
    {
        p.Reparent(this);
        _avaiblePlayers.Enqueue(p);
    }

    private void LoadSoundSettings()
    {

        _soundSettings = JSONLoader.Instance.DeserializeJSONElement<Dictionary<SoundEffect.SoundType, int>>("settings", "sound_settings");
        foreach (KeyValuePair<SoundEffect.SoundType, int> entry in _soundSettings)
        {
            int busindex = AudioServer.GetBusIndex(entry.Key.ToString());
            if (busindex >= 0)
            {
                AudioServer.SetBusVolumeDb(busindex, Mathf.LinearToDb(entry.Value / 100f));
            }
        }
    }

    /// <summary>
    /// Plays a sound at the given location. It follows the <c> parentNode </c> as source.
    /// </summary>
    /// <param name="soundDictionary"></param>
    /// <param name="parentNode"></param>
    /// <param name="playIn3D"></param>
    /// <returns></returns>
    public AudioStreamPlayer3D PlaySoundIn3D(Dictionary<string, object> soundDictionary, Node3D parentNode)
    {
        if (_avaiblePlayers.Count > 0)
        {
            AudioStreamPlayer3D audioPlayer = _avaiblePlayers.Dequeue();

            // Set up player
            audioPlayer.AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.InverseDistance;
            audioPlayer.PitchScale = 1f + (float)soundDictionary["delta_pitch_scale"];
            audioPlayer.Stream = (AudioStream)soundDictionary["audio_stream"];
            SoundEffect.SoundType soundEffectType = (SoundEffect.SoundType)soundDictionary["sound_type"];
            audioPlayer.Bus = (StringName)soundEffectType.ToString();

            audioPlayer.Reparent(parentNode);
            audioPlayer.Position = Vector3.Zero;
            audioPlayer.Play();
            return audioPlayer;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Plays a sound without 3D attenuation.
    /// </summary>
    /// <param name="soundDictionary"></param>
    /// <returns></returns>
    public AudioStreamPlayer3D PlaySound(Dictionary<string, object> soundDictionary)
    {
        if (_avaiblePlayers.Count > 0 && soundDictionary != null)
        {
            AudioStreamPlayer3D audioPlayer = _avaiblePlayers.Dequeue();

            // Set up player
            audioPlayer.AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.Disabled;
            audioPlayer.PitchScale = 1f + (float)soundDictionary["delta_pitch_scale"];
            audioPlayer.Stream = (AudioStream)soundDictionary["audio_stream"];
            SoundEffect.SoundType soundEffectType = (SoundEffect.SoundType)soundDictionary["sound_type"];
            audioPlayer.Bus = (StringName)soundEffectType.ToString();

            audioPlayer.Play();
            return audioPlayer;
        }
        else
        {
            return null;
        }
    }
    public AudioStreamPlayer3D PlayMusic(Dictionary<string, object> soundDictionary)
    {
        AudioStreamPlayer3D audioPlayer;
        if (_musicPlayer != null && soundDictionary != null)
        {
            audioPlayer = _musicPlayer;
        }
        else if (_avaiblePlayers.Count > 0 && soundDictionary != null)
        {
            audioPlayer = _avaiblePlayers.Dequeue();
            _musicPlayer = audioPlayer;
        }
        else
        {
            return null;
        }
        audioPlayer.AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.Disabled;
        audioPlayer.PitchScale = 1f + (float)soundDictionary["delta_pitch_scale"];
        audioPlayer.Stream = (AudioStream)soundDictionary["audio_stream"];
        audioPlayer.Bus = SoundEffect.SoundType.MUSIC.ToString();

        audioPlayer.Play();
        _musicPlayer.Finished += _OnMusicFinished;

        return audioPlayer;
    }
    public void StopMusic()
    {
        if (_musicPlayer != null && _musicPlayer.IsPlaying())
        {
            _musicPlayer.Finished -= _OnMusicFinished;
            _musicPlayer.Playing = false;
        }
    }
    public bool IsMusicPlaying()
    {
        if (_musicPlayer != null)
        {
            return _musicPlayer.Playing;
        }
        else
        {
            return false;
        }
    }

    private void _OnMusicFinished()
    {
        _musicPlayer.Finished -= _OnMusicFinished;
        _musicPlayer = null;
        _musicRestartTimer.Start();
    }
}