using Godot;
using System;

[GlobalClass]
public partial class TickableSoundEffect : SoundEffect
{
    [Export] public ITickable.SignalType signalType;
    [Export] public bool allowOnDisabled = true; 
    
}