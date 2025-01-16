using Godot;
using System;

public partial class TurnPanelUIController : PanelContainer
{
	[Export] private Label _turnNameLabel;
	[Export] private AnimationPlayer _anim;
	private string turnName;
	public void ChangeTurn(string name){
		turnName = name;
		_anim.Play("change_turn");
	}
	private void ChangeTurnLabel(){
		_turnNameLabel.Text = turnName;
	}
}
