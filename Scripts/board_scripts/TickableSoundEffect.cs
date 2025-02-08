using Godot;
using System;

[GlobalClass]
public partial class TickableSoundEffect : TickableEffect
{
    [Export] public bool allowOnDisabled = true; 
}