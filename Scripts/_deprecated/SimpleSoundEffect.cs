/*using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

[GlobalClass]
public partial class SimpleSoundEffect : BaseSoundEffect
{
    [Export] public AudioStream audioStream;
    public override Dictionary<string, object> GetSoundData()
    {
        Dictionary<string, object> audioSetup = new()
        {
            { "audio_stream", audioStream },
            { "delta_pitch_scale", RandomGenerator.Instance.GetRandFloatInRange(settings.deltaPitchScale.X, settings.deltaPitchScale.Y) }
        };

        return audioSetup;
    }

}*/
