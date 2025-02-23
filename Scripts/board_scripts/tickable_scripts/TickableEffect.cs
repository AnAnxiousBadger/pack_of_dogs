using Godot;
using System;
[GlobalClass]
public abstract partial class TickableEffect : Resource // Not needed for now, only TickableVisualEffect childclass in used
{
    [Export] public ITickable.SignalType signaltype = ITickable.SignalType.PRESSED;
    [Export] public string effectName;
}