using Godot;
using System;
using System.Collections.Generic;

public partial class DiceController : Node
{
	// REFERENCES
	private GameController _gameController;
	private UIController _uiController;
	private Timer _diceRollTimer;
	// OTHER
	private bool _canRoll = false;
	private RandomNumberGenerator _rand;
	public bool CanRoll {
		get { return _canRoll; }
		set {
			_canRoll = value; 
			_uiController.SetDiceRollButtonActivity(value);
		}
	}
	// SIGNALS
	[Signal] public delegate void DiceRolledEventHandler(int roll);

	public void ReadyDiceController(GameController gameController){
		_gameController = gameController;
		_uiController = gameController.uiController;
		_diceRollTimer = (Timer) GetNode("dice_roll_timer");
		_rand = new();
		_diceRollTimer.Timeout += _OnDiceRollTimerTimeout;
		_uiController.DiceRollButtonUp += _OnRollDiceRollButtonUp;
	}

    private int RollDice(RollSettings settings){
		int totalWeight = 0;
		List<int> partialWeights = new();
		for (int i = 0; i < settings.rollChances.Length; i++)
		{
			totalWeight += settings.rollChances[i].weight;
			partialWeights.Add(totalWeight);
		}

		int rollResult = _rand.RandiRange(1, totalWeight);

		int roll = int.MinValue;
		for (int i = 0; i < partialWeights.Count; i++)
		{
			if(rollResult <= partialWeights[i]){
				roll = settings.rollChances[i].result;
				break;
			}
		}

		return roll;
	}

	private void _OnRollDiceRollButtonUp(){
		_uiController.SetDiceRollLabelRolling();
		_diceRollTimer.Start();
	}

	private void _OnDiceRollTimerTimeout(){
		int roll = RollDice(_gameController.currPlayer.rollSettings);
		_uiController.SetDiceRollLabel(roll);
		EmitSignal(SignalName.DiceRolled, roll);
	}
}
