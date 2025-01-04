using Godot;
using System;

public partial class BoardNodeVisualEffectController : Node3D
{
	// http://psd.museum.upenn.edu/nepsd-frame.html
	private AnimationPlayer _anim;
	private string animation;
	public override void _Ready()
	{
		_anim = GetNode("AnimationPlayer") as AnimationPlayer;
		animation = _anim.GetAnimationList()[1];
	}

	public void PlayVisualEffect(){
		_anim.Play(animation);
	}

	private void EndEffect(){
		QueueFree();
	}
}
