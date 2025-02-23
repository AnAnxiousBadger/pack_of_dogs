using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class SoundEffect : Resource
{
    public enum SoundType {NONE, MUSIC, EFFECT, AMBIANCE, DIALOGUE}
    [Export] public string identifier;
    [Export] public SoundType soundType = SoundType.NONE;
    [Export] public AudioStream[] audioStreams;
    [Export] public SoundEffectSettings settings;

    public Dictionary<string, object> GetSoundData()
    {
        Dictionary<string, object> SoundDataDict = new()
        {
            { "audio_stream", audioStreams[RandomGenerator.Instance.GetRandIntInRange(0, audioStreams.Length - 1)] },
            { "delta_pitch_scale", RandomGenerator.Instance.GetRandFloatInRange(settings.deltaPitchScale.X, settings.deltaPitchScale.Y) },
            { "sound_type", soundType }
        };
        
        return SoundDataDict;
    }

    public SoundEffect(){
        this.identifier = "";
        this.settings = new SoundEffectSettings();
        this.audioStreams = Array.Empty<AudioStream>();
    }
}
