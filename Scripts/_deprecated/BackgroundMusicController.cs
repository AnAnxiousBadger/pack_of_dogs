using Godot;
using System;

public partial class BackgroundMusicController : AudioStreamPlayer
{
	[Export] private Timer _musicRestartTimer;
	[Export] private bool mute = true;

	public override void _Ready()
	{
		_musicRestartTimer.Timeout += StartMusic;
		this.Finished += () => _musicRestartTimer.Start();
		if(!mute){
			StartMusic();
		}
		
	}
	
	private void StartMusic(){
		Playing = true;
	}
}
