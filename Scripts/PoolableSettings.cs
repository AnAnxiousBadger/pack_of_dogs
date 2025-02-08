using Godot;
using System;
[GlobalClass]
public partial class PoolableSettings : Resource
{
    [Export] public string identifier;
    [Export] public int poolCount;
    [Export] public PackedScene scene;
}