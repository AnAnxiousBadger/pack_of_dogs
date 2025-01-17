using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StatsUIController : PanelContainer
{	
	// http://www.codex99.com/typography/1.html

	[Export] private Label _tableTitle;
	[ExportCategory("Statistics containers")]
	[Export] private StatsContainer _playerNameContainer;
	[Export] private StatsContainer _piecesDeliveredContainer;
	[Export] private StatsContainer _rollStatsContainer;
	[Export] private StatsContainer _distanceMovedContainer;
	[Export] private StatsContainer _enemyPiecesHitContainer;
	[Export] private StatsContainer _piecesHitContainer;
	[Export] private StatsContainer _turnsSkippedContainer;
	[Export] private StatsContainer _luckyScoreContainer;

	[ExportCategory("UI prefabs")]
	[Export] private PackedScene _labelType01;
	[Export] private PackedScene _rollStatsDisplay;

	private BasePlayerController _winner = null;

    public override void _Ready()
    {
        Visible = false;
    }
    public override void _Process(double delta)
    {
		if(Input.IsActionJustPressed("tab")){
			if(!Visible){
				GetAndDisplayStats(); 
			}
			if(_winner != null){
				SetStatTableVisibility(!Visible, "End of Game Summary"); // SET CLICKABILITY SET CROWN
			}	
			else{
				SetStatTableVisibility(!Visible, "Player Statistics");
			}
		}
    }
    public void AddPlayerStatsToUI(Dictionary<string, object> stats){
		// Set player name
		_playerNameContainer.AddStat(stats["player_name"].ToString(), _labelType01);

		// Set delivered pieces
		_piecesDeliveredContainer.AddStat(stats["pieces_delivered"].ToString(), _labelType01);

		// Set roll statistics
		RollSettings rollSettings = (RollSettings)stats["roll_settings"];
		List<int> rollables = rollSettings.GetRollables();

		List<int> rolls = (List<int>)stats["rolls"];
		float rollAverage = float.MinValue;
		if(rolls.Count > 0){
			rollAverage = (float)rolls.Average();
		}

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
			float percent = 0;
			if(rolls.Count > 0){
				percent = (float)rollCount[i] / (float)rolls.Count * 100f;
			}
			rollPercent.Add(percent);
		}

		_rollStatsContainer.AddStat(rollables, rollPercent, (int)stats["total_turns"], rolls.Count, rollAverage, _rollStatsDisplay);

		// Set distance moved
		_distanceMovedContainer.AddStat(stats["distance_moved"].ToString(), _labelType01);

		// Set enemy pieces  name
		_enemyPiecesHitContainer.AddStat(stats["enemy_pieces_hit"].ToString(), _labelType01);

		// Set  pieces  name
		_piecesHitContainer.AddStat(stats["pieces_hit"].ToString(), _labelType01);

		// Set  turns skipped
		_turnsSkippedContainer.AddStat(stats["turns_skipped"].ToString(), _labelType01);

		// Set lucky csore
		float score = (float)stats["lucky_score"];
		_luckyScoreContainer.AddStat(score.ToString("F2"), _labelType01);
	}

	private void SetStatTableVisibility(bool visible, string title){
		_tableTitle.Text = title;
		Visible = visible;
	}
	private void GetAndDisplayStats(){
		// Clear table
		_playerNameContainer.ClearContainer();
		_piecesDeliveredContainer.ClearContainer();
		_rollStatsContainer.ClearContainer();
		_distanceMovedContainer.ClearContainer();
		_enemyPiecesHitContainer.ClearContainer();
		_piecesHitContainer.ClearContainer();
		_turnsSkippedContainer.ClearContainer();
		_luckyScoreContainer.ClearContainer();

		// Display stats
		foreach (BasePlayerController player in GameController.Instance.players)
		{
			AddPlayerStatsToUI(player.playerStats.GetStats());
		}
	}

	private void DisplayEndTable(){

	}
}
