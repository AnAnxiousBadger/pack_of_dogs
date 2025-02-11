using Godot;
using System;
using System.Collections.Generic;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set;}
    [Export] private int _streamPlayerNum = 16;
    private string _bus = "Master";
    private Queue<AudioStreamPlayer3D> _avaiblePlayers;
    private AudioStreamPlayer _backgroundMusicController;
    private Timer _musicRestartTimer;
    [Export] private bool _muteMusic = false;
    [Export] private float _replayDelay = 15f;

    public override void _EnterTree()
    {
        if(Instance != null){
            this.QueueFree();
            return;
        }
        Instance = this;
    }
    public override void _Ready(){
        // Set up background music
        Timer _musicRestartTimer = new();
        AddChild(_musicRestartTimer);
        _musicRestartTimer.WaitTime = _replayDelay;
        _musicRestartTimer.OneShot = true;

        _backgroundMusicController = new();
        AddChild(_backgroundMusicController);
        _backgroundMusicController.Stream = GD.Load<AudioStream>("res://Assets/sounds/audio_streams/background_music.mp3");

        _musicRestartTimer.Timeout += StartMusic;
        _backgroundMusicController.Finished += () => _musicRestartTimer.Start();
        

        // Set up sound queue
        _avaiblePlayers = new();

		for (int i = 0; i < _streamPlayerNum; i++)
		{
			AudioStreamPlayer3D ap = new();
			AddChild(ap);
			_avaiblePlayers.Enqueue(ap);
			ap.Bus = _bus;
			ap.Finished += () => {_OnStreamFinished(ap); };
		}
    }

    private void _OnStreamFinished(AudioStreamPlayer3D p){
		p.Reparent(this);
		_avaiblePlayers.Enqueue(p);
	}

    public AudioStreamPlayer3D PlaySound(Dictionary<string, object> soundDictionary, Node3D parentNode, bool playIn3D){
        if(_avaiblePlayers.Count > 0){
            AudioStreamPlayer3D audioPlayer = _avaiblePlayers.Dequeue();

            // Set up player
            if(playIn3D){
                audioPlayer.AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.InverseDistance;
            }
            else{
                audioPlayer.AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.Disabled;
            }
            audioPlayer.PitchScale = 1f + (float)soundDictionary["delta_pitch_scale"];
            audioPlayer.Stream = (AudioStream)soundDictionary["audio_stream"];

            audioPlayer.Reparent(parentNode);
            audioPlayer.Position = Vector3.Zero;
            audioPlayer.Play();
            return audioPlayer;
        }
        else{
            return null;
        }        
	}

    public void StartMusic(){
        if(!_muteMusic){
            _backgroundMusicController.Playing = true;
        }
		
	}
    public void StopMusic(){
        _backgroundMusicController.Playing = false;
    }
    public bool IsMusicPlaying(){
        return _backgroundMusicController.Playing;
    }

    
}