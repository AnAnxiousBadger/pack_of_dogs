using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class RollButtonController : TickableButtonController
{
	public override void _Ready()
	{
		base._Ready();
		_isActive = true;
		_tickable.isActive = true;
		GameController.Instance.OnRollButtonActivityChange += ChangeActivity;
	}

	protected override void _OnButtonHovered(Vector3 pos){
		if(_isActive){
			_anim.Play("roll/on_hover");
		}
		else{
			_anim.Play("roll/on_disabled_hover");
		}
	}

	protected override void _OnButtonPressed(Vector3 pos, bool tickableIsActive){
		if(_isActive){
			_anim.Play("roll/on_pressed");
		}
	}
	protected override void _OnButtonReleased(Vector3 pos, bool tickableIsActive){
		if(_isActive){
			ChangeActivity(false);
			GameController.Instance.RollButtonUsed();
		}
	}
    protected override void _OnButtonPressStopped(bool tickableIsActive)
    {
		if(_isActive){
			_anim.Play("roll/RESET");
		} 
    }

    protected override void DisAbleRollButton(){
		_anim.Play("roll/on_disable");
	}
	protected override void EnableRollButton(){
		_anim.Play("roll/on_enable");
	}

}
