using Godot;
using System;

public partial class DiceDisplayController : TickableButtonController
{
	[Export] private RollButtonController _rollButton;
	[Export] private MeshInstance3D[] _numberMeshes;
	[Export] private GpuParticles3D _dustParticles;
	private int roll;
	public override void _Ready()
	{
		IsActive = true;
		HideNumbers();
		_rollButton.OnPressedRoll += _OnRollPressed;
		_rollButton.OnRollPressStopped += _OnRollPressStopped;
		GameController.Instance.diceController.DiceRolled += _OnDiceRolled;

	}
	public override void OnHovered(Vector3 pos){
		base.OnHovered(pos);
		if(IsActive && GameController.Instance.allowClicksOnTickableButtons){
			if(_anim.CurrentAnimation == "roll_animation"){
				HideNumbers();
				ShowRollNumber();
				_dustParticles.Emitting = false;
			}
			_anim.Play("hover_animation");
		}
	}
    public override void OnPressed(Vector3 pos){}

    private void _OnRollPressed(Vector3 pos, bool isRollActive){
		if(IsActive && isRollActive){
			_anim.Play("pre_roll_animation");
		}
	}
	private void _OnRollPressStopped(bool tickableIsActive){
		if(IsActive){
			_anim.Play("RESET");
		}
	}
	private void _OnDiceRolled(int roll){
		this.roll = roll;
		if(IsActive){
			_anim.Play("roll_animation");
			AudioManager.Instance.PlaySound(GameController.Instance.boardController.boardElementsController.boardElementsAudioLibrary.GetSound("dice_roll"), this, false);
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
