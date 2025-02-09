using Godot;
using System;
using System.Collections.Generic;

public partial class UIAnimationComponent : Node
{
	public enum ScalePivot {CENTER, LEFT_CENTER}
	private ScalePivot _scalePivot = ScalePivot.CENTER;
	[Export] private UITweenAnimation[] _tweenAnimationList = Array.Empty<UITweenAnimation>();
	[Export] private bool _isResponsiveToMouse = false;

	private Control _parent;
	
	public override void _Ready()
	{
		_parent = (Control) GetParent();
		

		if(_isResponsiveToMouse){
			ConnectSignals();
		}
		
		CallDeferred("SetUp");
	}

	private void SetUp(){
		
		if(_scalePivot == ScalePivot.CENTER){
			_parent.PivotOffset = _parent.Size / 2;
		}
		else if(_scalePivot == ScalePivot.LEFT_CENTER){
			_parent.PivotOffset = new Vector2(_parent.Size.X / 2, 0f);
		}

		List<UITweenAnimation> hoverAnims = GetTweenAnimationsForSignal(UITweenAnimation.SignalType.HOVER);

		Tween baseTween = null;
		foreach (UITweenAnimation tweenAnim in hoverAnims)
		{
			AddtweenForAnim(baseTween, tweenAnim, true);
		}
	}
	private void ConnectSignals(){
		_parent.MouseEntered += OnHover;
		_parent.MouseExited += OnHoverEnded;
	}
	public void OnHover(){
		List<UITweenAnimation> hoverAnims = GetTweenAnimationsForSignal(UITweenAnimation.SignalType.HOVER);

		Tween baseTween = null;
		foreach (UITweenAnimation tweenAnim in hoverAnims)
		{
			AddtweenForAnim(baseTween, tweenAnim, false);
		}

		
	}
	public void OnHoverEnded(){
		List<UITweenAnimation> hoverAnims = GetTweenAnimationsForSignal(UITweenAnimation.SignalType.HOVER);

		Tween baseTween = null;
		foreach (UITweenAnimation tweenAnim in hoverAnims)
		{
			AddtweenForAnim(baseTween, tweenAnim, true);
		}
	}

	private Tween Addtween(Tween tween, Control target, string property, Variant value, double duration, Tween.TransitionType transition){
		tween ??= GetTree().CreateTween();
		tween.TweenProperty(target, property, value, duration).SetTrans(transition);
		return tween;
	}

	private List<UITweenAnimation> GetTweenAnimationsForSignal(UITweenAnimation.SignalType signalType){
		List<UITweenAnimation> returnList = new();
		foreach (UITweenAnimation tweenAnim in _tweenAnimationList)
		{
			if(tweenAnim.signalType == signalType){
				returnList.Add(tweenAnim);
			}
		}
		return returnList;
	}

	private void AddtweenForAnim(Tween baseTween, UITweenAnimation tweenAnim, bool toDefault){
		Control target = _parent;
		if(tweenAnim.targetRelativePath != "this"){
			try
			{
				target = _parent.GetNode(tweenAnim.targetRelativePath) as Control;				
			}
			catch
			{
				target = null;
				GD.Print("Node not found for tween animation");
			}
		}
		if(target != null){
			Variant value = tweenAnim.property.TargetValue;
			if(toDefault){
				value = tweenAnim.property.DefaultValue;
			}
			Tween tween = baseTween;
			tween = Addtween(tween, target, tweenAnim.property.Property, value, tweenAnim.tweenTime, tweenAnim.transitionType);
			//tween.Parallel();
		}
	}
}
