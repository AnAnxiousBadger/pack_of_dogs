using Godot;
using System;

public partial class SkipButtonController : TickableButtonController
{
	public override void _Ready()
	{
		base._Ready();
		_isActive = false;
		_tickable.isActive = false;
		_anim.Play("skip/face_down");
		GameController.Instance.OnSkipButtonActivityChange += ChangeActivity;
	}
	protected override void _OnButtonHovered(Vector3 pos){
		if(_isActive){
			_anim.Play("skip/on_hover");
		}
		else{
			_anim.Play("skip/on_disabled_hover");
		}
	}
	protected override void _OnButtonPressed(Vector3 pos, bool tickableIsActive){
		if(_isActive){
			_anim.Play("skip/on_pressed");
		}
	}
	protected override void _OnButtonReleased(Vector3 pos, bool tickableIsActive){
		if(_isActive){
			ChangeActivity(false);
			GameController.Instance.SkipButtonUsed();
		}
	}
	protected override void _OnButtonPressStopped(bool tickableIsActive)
    {
		if(_isActive){
			_anim.Play("skip/RESET");
		} 
    }
	protected override void DisAbleRollButton(){
		_anim.Play("skip/on_disable");
	}
	protected override void EnableRollButton(){
		_anim.Play("skip/on_enable");
	}
}
