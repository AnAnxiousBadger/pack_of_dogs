using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class AudioLibrary : Resource
{
    [Export] public BaseSoundEffect[] soundEffects; // → Make it somehow into a disctionary

    public Dictionary<string, object> GetSound(string tag){
        for (int i = 0; i < soundEffects.Length; i++)
        {
            if(soundEffects[i].identifier == tag){
                return soundEffects[i].GetSound();
            }
        }
        return null;
    }
}
