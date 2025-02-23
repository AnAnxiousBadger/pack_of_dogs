using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class RollButtonController : TickableButtonController
{
	[Signal] public delegate void OnPressedRollEventHandler(Vector3 pos, bool isActive);
	[Signal] public delegate void OnRollPressStoppedEventHandler(bool isActive);
	public override void _Ready()
	{
		IsActive = true;
		GlobalHelper.Instance.GameController.OnRollButtonActivityChange += ChangeActivity;
		GlobalHelper.Instance.GameController.boardController.boardElementsController.OnRollDiceWithoutClicking += _OnButtonPressedWithoutClicking;
	}

	public override void OnHovered(Vector3 pos){
		base.OnHovered(pos);
		if(IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("roll/on_hover");
		}
		else if(GlobalHelper.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("roll/on_disabled_hover");
		}
		
	}

	public override void OnPressed(Vector3 pos){
		base.OnPressed(pos);
		if(IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("roll/on_pressed");
		}
		EmitSignal(SignalName.OnPressedRoll, pos, IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons);
	}
	public override void OnReleased(Vector3 pos){
		base.OnReleased(pos);
		if(IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons){
			DoButtonReleaseAction();
		}
	}
	protected override void DoButtonReleaseAction(){
		ChangeActivity(false);
		GlobalHelper.Instance.GameController.RollButtonUsed();
	}
    public override void OnPressStopped()
    {
		if(IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons){
			_anim.Play("roll/RESET");
		}
		EmitSignal(SignalName.OnRollPressStopped, IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons);
    }

    protected override void DisableButton(){
		base.DisableButton();
		_anim.Play("roll/on_disable");
	}
	protected override void EnableButton(){
		base.EnableButton();
		_anim.Play("roll/on_enable");
	}

}
