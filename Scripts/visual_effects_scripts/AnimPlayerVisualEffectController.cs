using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AnimPlayerVisualEffectController : VisualEffectController
{
	// http://psd.museum.upenn.edu/nepsd-frame.html
	// https://en.wiktionary.org/wiki/𒋰 - tab 𒋰 → double
	// https://sumerianlanguage.tumblr.com/post/164302453947/hi-ive-seen-you-do-a-few-sumerian-name - dul 𒊨 → protect
	// en.wiktionary.org/wiki/𒊨
	// https://en.wiktionary.org/wiki/𒍚#Sumerian - ud5, uzud → goat
	// https://oracc.museum.upenn.edu/epsd2/cbd/sux/o0034333.html - mu ša 𒅲𒁺 → bleat
	// https://oracc.museum.upenn.edu/epsd2/sux/o0037096 - sa dug 𒁲𒅗 → arrive

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
	public override void Play(Vector3 globalPos, Vector3 globalRot)
    {
        base.Play(globalPos, globalRot);
		_anim.Play(animation);
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }
}
