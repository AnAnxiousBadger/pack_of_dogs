using Godot;
using System;

public partial class SkipButtonController : TickableButtonController
{
	public override void _Ready()
	{
		IsActive = false;
		_anim.Play("skip/face_down");
		GlobalClassesHolder.Instance.GameController.OnSkipButtonActivityChange += ChangeActivity;
		GlobalClassesHolder.Instance.GameController.boardController.boardElementsController.OnSkipWithoutClicking += _OnButtonPressedWithoutClicking;
	}
	public override void OnHovered(Vector3 pos){
		base.OnHovered(pos);
		if(IsActive && GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("skip/on_hover");
		}
		else if(GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("skip/on_disabled_hover");
		}
	}
	public override void OnPressed(Vector3 pos){
		base.OnPressed(pos);
		if(IsActive && GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("skip/on_pressed");
		}
	}
	public override void OnReleased(Vector3 pos){
		base.OnReleased(pos);
		if(IsActive && GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons){
			DoButtonReleaseAction();
		}
	}
	protected override void DoButtonReleaseAction(){
		ChangeActivity(false);
		GlobalClassesHolder.Instance.GameController.SkipButtonUsed();
	}
	public override void OnPressStopped()
    {
		if(IsActive && GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("skip/RESET");
		} 
    }
	protected override void DisableButton(){
		_anim.Play("skip/on_disable");
	}
	protected override void EnableButton(){
		_anim.Play("skip/on_enable");
	}
}
