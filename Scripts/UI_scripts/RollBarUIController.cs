using Godot;
using System;

public partial class RollBarUIController : VBoxContainer
{
	[Export] private Label _rollPercentLabel;
	[Export] private Label _rollLabel;
	[Export] private ProgressBar _rollbar;

	public void SetRollBar(int rollable, float rollPercent){
		_rollPercentLabel.Text = rollPercent.ToString("F1");
		_rollLabel.Text = rollable.ToString();
		_rollbar.Value = rollPercent;

	}
}
