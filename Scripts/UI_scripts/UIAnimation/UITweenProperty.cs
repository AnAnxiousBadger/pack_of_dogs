using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public abstract partial class UITweenProperty : Resource
{
    public abstract string Property { get; }
    public abstract Variant DefaultValue { get;}
    public abstract Variant TargetValue { get;}
}