using Godot;
using System;
using System.Collections.Generic;

public partial class DiceController : Node
{
	// OTHER
	private bool _canRoll = false;
	public bool CanRoll {
		get { return _canRoll; }
		set {
			_canRoll = value;
			GlobalClassesHolder.Instance.GameController.ChangeRollButtonActivity(value);
		}
	}
	// SIGNALS
	[Signal] public delegate void DiceRolledEventHandler(int roll);

	public void ReadyDiceController(){
		GlobalClassesHolder.Instance.GameController.OnRollButtonUsed += _OnRollDiceRollButtonUp;
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

	private void _OnRollDiceRollButtonUp(){
		CanRoll = false;
		int roll = RollDice(GlobalClassesHolder.Instance.GameController.currPlayer.rollSettings);
		EmitSignal(SignalName.DiceRolled, roll);
	}

}
