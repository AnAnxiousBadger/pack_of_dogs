using Godot;
using System;

[GlobalClass]
public partial class RollSettings : Resource
{
    [Export] public RollChance[] rollChances;
}