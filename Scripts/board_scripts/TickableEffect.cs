using Godot;
using System;
[GlobalClass]
public abstract partial class TickableEffect : Resource
{
    public enum SignalType {PRESSED, RELEASED, HOVERED, ENABLE, DISABLE}
    [Export] public SignalType signaltype = SignalType.PRESSED;
    [Export] public string effectName;
}