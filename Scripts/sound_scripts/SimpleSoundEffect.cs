using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

[GlobalClass]
public partial class SimpleSoundEffect : BaseSoundEffect
{
    [Export] public AudioStream sound;
    public override Dictionary<string, object> GetSound()
    {
        Dictionary<string, object> audioSetup = new()
        {
            { "audio_stream", sound },
            { "delta_pitch_scale", RandomGenerator.Instance.GetRandFInRange(settings.deltaPitchScale.X, settings.deltaPitchScale.Y) }
        };

        return audioSetup;
    }

}
