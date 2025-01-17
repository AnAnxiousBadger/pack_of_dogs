using Godot;
using System;
using System.Collections.Generic;

public partial class RollStatsDisplayUIController : VBoxContainer
{
	[Export] private PackedScene _rollBar;
	[Export] private HBoxContainer _rollBarContainer;
	[Export] private Label _totalTurnsLabel;
	[Export] private Label _totalRollsLabel;
	[Export] private Label _averageRollLabel;

	public void SetRollBars(List<int> rollables, List<float> rollPercents, int totalTurns, int totalRolls, float averageRoll){
		
		for (int i = 0; i < rollables.Count; i++)
		{
			RollBarUIController bar = _rollBar.Instantiate() as RollBarUIController;
			_rollBarContainer.AddChild(bar);
			bar.SetRollBar(rollables[i], rollPercents[i]);
		}

		_totalTurnsLabel.Text = totalTurns.ToString();

		_totalRollsLabel.Text = totalRolls.ToString();

		if(averageRoll != float.MinValue){
			_averageRollLabel.Text = averageRoll.ToString("F2");
		}
		else{
			_averageRollLabel.Text = "-";
		}
		
	}
}
