using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class RandomSoundEffect : BaseSoundEffect
{
    [Export] public AudioStream[] sounds;
    public override Dictionary<string, object> GetSound()
    {
        Dictionary<string, object> audioSetup = new()
        {
            { "audio_stream", sounds[RandomGenerator.Instance.GetRandIntInRange(0, sounds.Length - 1)] },
            { "delta_pitch_scale", RandomGenerator.Instance.GetRandFInRange(settings.deltaPitchScale.X, settings.deltaPitchScale.Y) }
        };
        
        return audioSetup;
    }
}
