using Godot;
using System;

[GlobalClass]
public partial class TickableEffect: Resource
{
    public enum SignalType {PRESSED, RELEASED}
    [Export] public SignalType signaltype = SignalType.PRESSED;
    [Export] public string effectName;
    [Export] public bool onClickPosition = false;
    [Export] public bool allowOnDisabledTickableButtonClicks = true;
}