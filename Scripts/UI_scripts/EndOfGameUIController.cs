using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EndOfGameUIController : PanelContainer
{	
	[Export] BasePlayerController _player; // TEMPORARY
	[ExportCategory("Statistics containers")]
	[Export] private HBoxContainer _playerNameContainer;
	[Export] private HBoxContainer _piecesDeliveredContainer;
	[Export] private HBoxContainer _rollStatsContainer;
	[Export] private HBoxContainer _distanceMovedContainer;
	[Export] private HBoxContainer _enemyPiecesHitContainer;
	[Export] private HBoxContainer _piecesHitContainer;
	[Export] private HBoxContainer _turnsSkippedContainer;
	[Export] private HBoxContainer _luckyScoreContainer;

	[ExportCategory("UI prefabs")]
	[Export] private PackedScene _labelType01;
	[Export] private PackedScene _rollStatsDisplay;

    public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("test")){
			AddPlayerStatsToUI(_player.playerStats.GetStats());
		}
    }
    public void AddPlayerStatsToUI(Dictionary<string, object> stats){
		// Set player name
		Label playerNameLabel = _labelType01.Instantiate() as Label;
		playerNameLabel.Text = stats["player_name"].ToString();
        _playerNameContainer.AddChild(playerNameLabel);

		// Set delivered pieces
		Label piecesDeliveredLabel = _labelType01.Instantiate() as Label;
		piecesDeliveredLabel.Text = stats["pieces_delivered"].ToString();
		_piecesDeliveredContainer.AddChild(piecesDeliveredLabel);

		// Set roll statistics
		RollSettings rollSettings = (RollSettings)stats["roll_settings"];
		List<int> rollables = rollSettings.GetRollables();

		List<int> rolls = (List<int>)stats["rolls"];
		float rollAverage = (float)rolls.Average();

		List<int> rollCount = new();
		for (int i = 0; i < rollables.Count; i++)
		{
			int num = 0;
			for (int j = 0; j < rolls.Count; j++)
			{
				if(rolls[j] == rollables[i]){
					num += 1;
				}
			}
			rollCount.Add(num);
		}

		List<float> rollPercent = new();
		for (int i = 0; i < rollCount.Count; i++)
		{
			rollPercent.Add((float)rollCount[i] / (float)rolls.Count * 100f);
		}


		RollStatsDisplayUIController rollStatsDisplay = _rollStatsDisplay.Instantiate() as RollStatsDisplayUIController;
		_rollStatsContainer.AddChild(rollStatsDisplay);
		rollStatsDisplay.SetRollBars(rollables, rollPercent, rollAverage);

		// Set distance moved
		Label distanceMovedlabel = _labelType01.Instantiate() as Label;
		distanceMovedlabel.Text = stats["distance_moved"].ToString();
        _distanceMovedContainer.AddChild(distanceMovedlabel);

		// Set enemy pieces  name
		Label enemyPiecesHitLabel = _labelType01.Instantiate() as Label;
		enemyPiecesHitLabel.Text = stats["enemy_pieces_hit"].ToString();
        _enemyPiecesHitContainer.AddChild(enemyPiecesHitLabel);

		// Set  pieces  name
		Label piecesHitLabel = _labelType01.Instantiate() as Label;
		piecesHitLabel.Text = stats["pieces_hit"].ToString();
        _piecesHitContainer.AddChild(piecesHitLabel);

		// Set  turns skipped
		Label turnsSkipped = _labelType01.Instantiate() as Label;
		turnsSkipped.Text = stats["turns_skipped"].ToString();
        _turnsSkippedContainer.AddChild(turnsSkipped);

		// Set lucky csore
		Label luckyScorelabel = _labelType01.Instantiate() as Label;
		float score = (float)stats["lucky_score"];
		luckyScorelabel.Text = score.ToString("F2");
        _luckyScoreContainer.AddChild(luckyScorelabel);
	}

	
}
