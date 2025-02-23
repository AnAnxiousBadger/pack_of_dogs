using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class AudioLibrary : Resource
{
    [Export] public SoundEffect[] soundEffects;

    public Dictionary<string, object> GetSound(string tag){
        for (int i = 0; i < soundEffects.Length; i++)
        {
            if(soundEffects[i].identifier == tag){
                return soundEffects[i].GetSoundData();
            }
        }

        return null;
    }
    public Dictionary<string, object> GetRandomSound(){
        if(soundEffects.Length >= 0){
            return soundEffects[RandomGenerator.Instance.GetRandIntInRange(0, soundEffects.Length - 1)].GetSoundData();
        }
        else{
            return null;
        }
        
    }

    public List<Dictionary<string, object>> GetAllSoundsOfType(Func<SoundEffect, bool> predicate){
        List<Dictionary<string, object>> soundDatasOfType = new();

        List<SoundEffect> soundEffectsOfType =  soundEffects.Where(predicate).ToList();
        foreach (SoundEffect soundEffect in soundEffectsOfType)
        {
            soundDatasOfType.Add(soundEffect.GetSoundData());
        }

        return soundDatasOfType;
    }

    public List<Dictionary<string, object>> GetAllSoundsOfType(Func<TickableSoundEffect, bool> predicate){
        List<Dictionary<string, object>> soundDatasOfType = new();

        List<TickableSoundEffect> tickableSoundEffects = soundEffects.OfType<TickableSoundEffect>().ToList();
        List<TickableSoundEffect> soundEffectsOfType =  tickableSoundEffects.Where(predicate).ToList();
        foreach (SoundEffect soundEffect in soundEffectsOfType)
        {
            soundDatasOfType.Add(soundEffect.GetSoundData());
        }

        return soundDatasOfType;
    }

    public List<SoundEffect> GetAllSoundEffectsOfType(Func<SoundEffect,bool> predicate){
        return soundEffects.Where(predicate).ToList();
    }

    public List<TickableSoundEffect> GetAllSoundEffectsOfType(Func<TickableSoundEffect, bool> predicate){
        return soundEffects.OfType<TickableSoundEffect>().Where(predicate).ToList();
    }
}
