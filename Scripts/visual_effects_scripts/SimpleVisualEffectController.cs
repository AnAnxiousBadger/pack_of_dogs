using Godot;
using System;

public partial class SimpleVisualEffectController : VisualEffectController
{
    public override void Play(Vector3 globalPos)
    {
        base.Play(globalPos);
    }
    public override void Play(Vector3 globalPos, Vector3 globalRot)
    {
        base.Play(globalPos, globalRot);
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }
}