using Godot;
using System;

[GlobalClass]
public partial class UITweenIntProperty : UITweenProperty
{
    [Export] private string _property;
    public override string Property { get { return _property; } }
    [Export] private int _defaultValue;
    [Export] private int _targetValue;
    public override Variant DefaultValue { get {return _defaultValue;} }
    public override Variant TargetValue { get {return _targetValue;} }
}