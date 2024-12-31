using Godot;
using System;

public partial class TickableController : StaticBody3D
{
	// EXPORTS
	[Export] private bool _hasEnableAnim; // on_enable
	[Export] private bool _hasDisableAnim; // on_disbale
	[Export] private bool _hasPressedAnim; // on_pressed
	[Export] private bool _hasPressedDisabledAnim; // on_disabled_pressed
	[Export] private bool _hasHoverAnim; // on_hover
	[Export] private bool _hasHoverDisabledAnim; // on_disabled_hover
	// REFERENCES
	private AnimationPlayer _anim;
	// OTHER
	private bool _isActive = true;
	public bool IsActive{
		get{ return _isActive; }
		set{
			_isActive = value; 
			if(value && _hasEnableAnim){
			_anim?.Play("on_enable");
		}
		else if(!value && _hasDisableAnim){
			_anim?.Play("on_disable");
		}
		}
	}
	// SIGNALS
	[Signal] public delegate void OnPressedTickableEventHandler(Vector3 pos);
	[Signal] public delegate void OnReleasedTickableEventHandler();
	[Signal] public delegate void OnHoveredTickableEventHandler();

    public override void _Ready()
    {
        _anim = GetNode("AnimationPlayer") as AnimationPlayer;
    }

	public void OnHover(){
		EmitSignal(SignalName.OnHoveredTickable);
		if(IsActive && _hasHoverAnim){
			_anim.Play("on_hover");
		}
		else if(!IsActive && _hasHoverDisabledAnim){
			_anim.Play("on_disabled_hover");
		}
	}

	public void OnPressed(Vector3 pos){
		if(IsActive){
			EmitSignal(SignalName.OnPressedTickable, pos);
			if(_hasPressedAnim){
				_anim.Play("on_pressed");
			}
		}
		else if(!IsActive){
			if(_hasPressedDisabledAnim){
				_anim.Play("on_disable_pressed");
			}
		}
	}
	public void OnReleased(){
		if(IsActive){
			EmitSignal(SignalName.OnReleasedTickable);
		}
		
	}
}
