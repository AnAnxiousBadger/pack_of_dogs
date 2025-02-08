using Godot;
using System;
[GlobalClass]
public partial class SoundEffectSettings : Resource
{
    [Export] public Vector2 deltaPitchScale;

    public SoundEffectSettings()
    {
        this.deltaPitchScale = Vector2.Zero;
    }
}