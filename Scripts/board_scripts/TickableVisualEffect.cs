using Godot;
using System;

[GlobalClass]
public partial class TickableVisualEffect: TickableEffect
{
    [Export] public bool onClickPosition = false;
    [Export] public bool allowOnDisabledTickableButtonClicks = true;
}