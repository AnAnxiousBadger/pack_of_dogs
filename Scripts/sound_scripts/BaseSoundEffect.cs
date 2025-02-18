using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseSoundEffect : Resource
{
    public enum SoundType {NONE, MUSIC, EFFECT, AMBIANCE, DIALOGUE}
    [Export] public string identifier;
    [Export] public SoundType soundType = SoundType.NONE;
    [Export] public SoundEffectSettings settings;

    public abstract Dictionary<string, object> GetSound();

    public BaseSoundEffect(){
        this.identifier = "";
        this.settings = new SoundEffectSettings();
    }
}
