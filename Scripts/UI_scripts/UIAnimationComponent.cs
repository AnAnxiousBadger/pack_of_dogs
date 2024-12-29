using Godot;
using System;

public partial class UIAnimationComponent : Node
{
	[Export] private bool _fromCenter = true;
	[Export] private Vector2 _hoverScale;
	[Export] private float time = 0.1f;
	[Export] private Tween.TransitionType _transitionType;

	private Control _target;
	private Vector2 _defaultScale;
	public override void _Ready()
	{
		_target = (Control) GetParent();

		ConnectSignals();
		CallDeferred("SetUp");
	}

	private void SetUp(){
		if(_fromCenter){
			_target.PivotOffset = _target.Size / 2;
		}
		_defaultScale = _target.Scale;
	}
	private void ConnectSignals(){
		_target.MouseEntered += OnHover;
		_target.MouseExited += OnHoverEnded;
	}
	private void OnHover(){
		Addtween("scale", _hoverScale, time);
	}
	private void OnHoverEnded(){
		Addtween("scale", _defaultScale, time);
	}

	private void Addtween(string property, Variant value, double duration){
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(_target, property, value, duration).SetTrans(_transitionType);
	}
}
