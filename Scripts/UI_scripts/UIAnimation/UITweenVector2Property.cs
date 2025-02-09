using Godot;
using System;

[GlobalClass]
public partial class UITweenVector2Property : UITweenProperty
{
    public enum Vector2Property {SCALE};
    [Export] private Vector2Property _property;
    public override string Property { get { return _property.ToString().ToLower(); } }
    [Export] private Vector2 _defaultValue;
    [Export] private Vector2 _targetValue;
    public override Variant DefaultValue { get {return _defaultValue;} }
    public override Variant TargetValue { get {return _targetValue;} }
}