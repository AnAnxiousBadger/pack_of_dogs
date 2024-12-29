using Godot;
using System;

public partial class UIController : CanvasLayer
{
	// EXPORTS
	[Export] private Label _diceRollLabel;
	[Export] private Button _diceRollButton;
	[Export] private Button _skipTurnButton;
	[Export] private Label _turnLabel;
	// SIGNALS
	[Signal] public delegate void DiceRollButtonUpEventHandler();
	[Signal] public delegate void SkipTurnButtonUpEventHandler();

	public void ReadyUIController(){
		_diceRollButton.ButtonUp += _OnDiceRollButtonUp;
		_skipTurnButton.ButtonUp += _OnSkipTurnButtonUp;
	}
	private void _OnDiceRollButtonUp(){
		EmitSignal(SignalName.DiceRollButtonUp);
	}
	private void _OnSkipTurnButtonUp(){
		EmitSignal(SignalName.SkipTurnButtonUp);
	}
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
	public void SetDiceRollButtonActivity(bool isActive){
		_diceRollButton.Disabled = !isActive;
	}
	public void SetSkipButtonActivity(bool isActive){
		_skipTurnButton.Disabled = !isActive;
	}
}
