using Godot;
using System;
[GlobalClass]
public partial class UITweenAnimation : Resource
{
    public enum SignalType {HOVER};
    [Export] public SignalType signalType = SignalType.HOVER;
    [Export] public string targetRelativePath;
    [Export] public UITweenProperty property;
    [Export] public float tweenTime = 0.1f;
    [Export] public Tween.TransitionType transitionType; 

}