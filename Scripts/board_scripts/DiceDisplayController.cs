using Godot;
using System;

public partial class DiceDisplayController : TickableButtonController
{
	[Export] private TickableController _rollButton;
	[Export] private MeshInstance3D[] _numberMeshes;
	private int roll;
	public override void _Ready()
	{
		_isActive = true;
		_tickable.isActive = true;
		HideNumbers();
		base._Ready();
		_rollButton.OnPressedTickable += _OnRollPressed;
		_rollButton.OnPressTickableStopped += _OnRollPressStopped;
		GameController.Instance.diceController.DiceRolled += _OnDiceRolled;

	}
	protected override void _OnButtonHovered(Vector3 pos){
		if(_isActive){
			if(_anim.CurrentAnimation == "roll_animation"){
				HideNumbers();
				ShowRollNumber();
			}
			_anim.Play("hover_animation");
		}
	}

	private void _OnRollPressed(Vector3 pos, bool tickableIsActive){
		if(_isActive && tickableIsActive){
			_anim.Play("pre_roll_animation");
		}
	}
	private void _OnRollPressStopped(bool tickableIsActive){
		if(_isActive){
			_anim.Play("RESET");
		}
	}
	private void _OnDiceRolled(int roll){
		this.roll = roll;
		if(_isActive){
			_anim.Play("roll_animation");
		}
	}

	private void HideNumbers(){
		for (int i = 0; i < _numberMeshes.Length; i++)
		{
			_numberMeshes[i].Visible = false;
		}
	}
	private void ShowRollNumber(){
		_numberMeshes[roll].Visible = true;
	}

}
