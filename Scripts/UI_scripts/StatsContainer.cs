using Godot;
using System;
using System.Collections.Generic;

public partial class StatsContainer : HBoxContainer
{
	public void ClearContainer(){
		Godot.Collections.Array<Node> children = GetChildren();
		if(children.Count > 1){
			for (int i = 1; i < children.Count; i++)
			{
				children[i].QueueFree();
			}
		}
	}
	public void AddStat(string statString, PackedScene labelScene){
		Label statLabel = labelScene.Instantiate() as Label;
		statLabel.Text = statString.ToString();
        AddChild(statLabel);
	}

	public void AddStat(List<int> rollables, List<float> rollPercent, int totalTurns, int totalRolls, float rollAverage, PackedScene rollDisplayScene){
		RollStatsDisplayUIController rollStatsDisplay = rollDisplayScene.Instantiate() as RollStatsDisplayUIController;
		rollStatsDisplay.SetRollBars(rollables, rollPercent, totalTurns, totalRolls, rollAverage);
		AddChild(rollStatsDisplay);
	}
}
