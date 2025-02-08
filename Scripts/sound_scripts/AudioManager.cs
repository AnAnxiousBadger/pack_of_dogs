using Godot;
using System;
using System.Collections.Generic;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get;  private set;}
    [Export] private int _streamPlayerNum = 16;
    private string _bus = "Master";
    private Queue<AudioStreamPlayer3D> _avaiblePlayers;

    public override void _Ready(){
        if(Instance != null){
			QueueFree();
			return;
		}
		Instance = this;

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

    
}