using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseSoundEffect : Resource
{
    [Export] public string identifier;
    [Export] public SoundEffectSettings settings;

    public abstract Dictionary<string, object> GetSound();

    public BaseSoundEffect(){
        this.identifier = "";
        this.settings = new SoundEffectSettings();
    }
}
