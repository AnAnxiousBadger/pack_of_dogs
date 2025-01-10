using Godot;
using System;
using System.Collections.Generic;

public partial class RollStatsDisplayUIController : VBoxContainer
{
	[Export] private PackedScene _rollBar;
	[Export] private HBoxContainer _rollBarContainer;
	[Export] private Label _averageRollLabel;

	public void SetRollBars(List<int> rollables, List<float> rollPercents, float averageRoll){
		for (int i = 0; i < rollables.Count; i++)
		{
			RollBarUIController bar = _rollBar.Instantiate() as RollBarUIController;
			_rollBarContainer.AddChild(bar);
			bar.SetRollBar(rollables[i], rollPercents[i]);
		}
		_averageRollLabel.Text = averageRoll.ToString("F2");
	}
}
