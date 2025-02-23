/*using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class RandomSoundEffect : BaseSoundEffect
{
    [Export] public AudioStream[] audioStreams;
    public override Dictionary<string, object> GetSoundData()
    {
        Dictionary<string, object> audioSetup = new()
        {
            { "audio_stream", audioStreams[RandomGenerator.Instance.GetRandIntInRange(0, audioStreams.Length - 1)] },
            { "delta_pitch_scale", RandomGenerator.Instance.GetRandFloatInRange(settings.deltaPitchScale.X, settings.deltaPitchScale.Y) }
        };
        
        return audioSetup;
    }
}*/
