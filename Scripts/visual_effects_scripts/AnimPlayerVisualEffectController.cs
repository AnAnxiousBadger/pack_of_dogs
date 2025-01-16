using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AnimPlayerVisualEffectController : VisualEffectController
{
	// http://psd.museum.upenn.edu/nepsd-frame.html
	// https://en.wiktionary.org/wiki/𒋰 - tab 𒋰 → double
	// https://sumerianlanguage.tumblr.com/post/164302453947/hi-ive-seen-you-do-a-few-sumerian-name - dul 𒊨 → protect
	//en.wiktionary.org/wiki/𒊨

	private AnimationPlayer _anim;
	private string animation;
	public override void _Ready()
	{
		_anim = GetNode("AnimationPlayer") as AnimationPlayer;
		animation = _anim.GetAnimationList()[1];
	}

    public override void Play(Vector3 globalPos)
    {
        base.Play(globalPos);
		_anim.Play(animation);
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }
}
