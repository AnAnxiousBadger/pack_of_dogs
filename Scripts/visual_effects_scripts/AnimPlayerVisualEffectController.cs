using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AnimPlayerVisualEffectController : VisualEffectController
{
	// http://psd.museum.upenn.edu/nepsd-frame.html
	private AnimationPlayer _anim;
	private string animation;
	public override void _Ready()
	{
		_anim = GetNode("AnimationPlayer") as AnimationPlayer;
		animation = _anim.GetAnimationList()[1];
	}

	/*public override void Play(){
		base.Play();
		_anim.Play(animation);
	}*/
    public override void Play(Vector3 globalPos)
    {
        base.Play(globalPos);
		_anim.Play(animation);
    }
}
