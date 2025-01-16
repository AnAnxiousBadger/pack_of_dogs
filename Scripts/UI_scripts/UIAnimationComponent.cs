using Godot;
using System;

public partial class UIAnimationComponent : Node
{
	public enum ScalePivot {CENTER, LEFT_CENTER}
	[Export] private ScalePivot _scalePivot = ScalePivot.CENTER;
	[Export] private Vector2 _hoverScale;
	[Export] private float time = 0.1f;
	[Export] private Tween.TransitionType _transitionType;
	[Export] private bool _isResponsiveToMouse = false;

	private Control _target;
	private Vector2 _defaultScale;
	public override void _Ready()
	{
		_target = (Control) GetParent();

		if(_isResponsiveToMouse){
			ConnectSignals();
		}
		
		CallDeferred("SetUp");
	}

	private void SetUp(){
		if(_scalePivot == ScalePivot.CENTER){
			_target.PivotOffset = _target.Size / 2;
		}
		else if(_scalePivot == ScalePivot.LEFT_CENTER){
			_target.PivotOffset = new Vector2(_target.Size.X / 2, 0f);
		}
		_defaultScale = _target.Scale;
	}
	private void ConnectSignals(){
		_target.MouseEntered += OnHover;
		_target.MouseExited += OnHoverEnded;
	}
	public void OnHover(){
		Addtween("scale", _hoverScale, time);
	}
	public void OnHoverEnded(){
		Addtween("scale", _defaultScale, time);
	}

	private void Addtween(string property, Variant value, double duration){
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(_target, property, value, duration).SetTrans(_transitionType);
	}
}
