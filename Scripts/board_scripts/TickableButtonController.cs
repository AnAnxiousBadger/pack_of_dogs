using Godot;
using System;

public abstract partial class TickableButtonController : Node
{
    [Export] protected TickableController _tickable;
	[Export] protected AnimationPlayer _anim;
    protected bool _isActive;
    public override void _Ready()
	{
		_tickable.OnHoveredTickable +=_OnButtonHovered;
		_tickable.OnPressedTickable +=_OnButtonPressed;
		_tickable.OnReleasedTickable +=_OnButtonReleased;
        _tickable.OnPressTickableStopped += _OnButtonPressStopped;
	}
    protected virtual void _OnButtonHovered(Vector3 pos){}
	protected virtual void _OnButtonPressed(Vector3 pos, bool tickableIsActive){}
	protected virtual void _OnButtonReleased(Vector3 pos, bool tickableIsActive){}
    protected virtual void _OnButtonPressStopped(bool tickableIsActive){}    
    protected virtual void DisAbleRollButton(){}
	protected virtual void EnableRollButton(){}

    protected void ChangeActivity(bool isActive){
		_tickable.isActive = isActive;

        if(isActive != _isActive){
            if(isActive){
                EnableRollButton();
            }
            else if(!isActive){
                DisAbleRollButton();
            }
        }
		_isActive = isActive;
	}
}