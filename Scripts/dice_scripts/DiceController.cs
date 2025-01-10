using Godot;
using System;
using System.Collections.Generic;

public partial class DiceController : Node
{
	// REFERENCES
	private GameController _gameController;
	private UIController _uiController;
	private Timer _diceRollTimer;
	private TickableController _rollButton;
	// OTHER
	private bool _canRoll = false;
	public bool CanRoll {
		get { return _canRoll; }
		set {
			_canRoll = value;
			_rollButton.IsActive = value;
		}
	}
	// SIGNALS
	[Signal] public delegate void DiceRolledEventHandler(int roll);

	public void ReadyDiceController(GameController gameController){
		_gameController = gameController;
		_uiController = gameController.uiController;
		_diceRollTimer = (Timer) GetNode("dice_roll_timer");
		_diceRollTimer.Timeout += _OnDiceRollTimerTimeout;
		_rollButton = _gameController.boardController.boardElementsController.rollButton;
		_rollButton.OnReleasedTickable += _OnRollDiceRollButtonUp;
	}

    private int RollDice(RollSettings settings){
		int totalWeight = 0;
		List<int> partialWeights = new();
		for (int i = 0; i < settings.rollChances.Length; i++)
		{
			totalWeight += settings.rollChances[i].weight;
			partialWeights.Add(totalWeight);
		}

		int rollResult = RandomGenerator.Instance.GetRandIntInRange(1, totalWeight);

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

	private void _OnRollDiceRollButtonUp(Vector3 hitPos){
		CanRoll = false;
		_uiController.SetDiceRollLabelRolling();
		_diceRollTimer.Start();
	}

	private void _OnDiceRollTimerTimeout(){
		int roll = RollDice(_gameController.currPlayer.rollSettings);
		_uiController.SetDiceRollLabel(roll);
		EmitSignal(SignalName.DiceRolled, roll);
	}
}
