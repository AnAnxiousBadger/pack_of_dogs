using Godot;
using System;

public partial class UIController : Control
{
	// EXPORTS
	[Export] private Label _diceRollLabel;
	[Export] private BaseButton _skipTurnButton;
	[Export] private Label _turnLabel;

	public void ReadyUIController(){}

    public void SetTurnLabel(string playerName){
		_turnLabel.Text = playerName + "'s Turn";
	}

	public void SetDiceRollLabel(int roll){
		_diceRollLabel.Text = roll.ToString();
	}
	public void SetDiceRollLabelRolling(){
		_diceRollLabel.Text = "...";
	}
	public void SetDiceRollLabelUnset(){
		_diceRollLabel.Text = "Roll!";
	}
}
