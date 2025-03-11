using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public partial class VictoryUIController : Control
{
	[Export] private UIController _UIController;
	[Export] private AnimationPlayer _anim;
	[Export] private Label _winnerText;
	[Export] private Label _quoteText;
	[Export] private Label _fatesTitle;
	[Export] private VBoxContainer _fatesContainer;
	[Export] private PackedScene _fateContainerScene;
	[ExportGroup("Buttons")]
	[Export] private Button _viewStatsButton;
	[Export] private Button _mainMenuButton;
	[Export] private Button _playAgainButton;
	public void SetUpVictoryUI()
    {
        _ = HideVictoryPanel(false);
		_viewStatsButton.ButtonUp += _OnViewStatsPressed;
		_viewStatsButton.ButtonDown += _UIController._OnButtonDown;

		// Needed check to run unique scene
		if(ScenesController.Instance != null){
			GameModeLevelController levelController = (GameModeLevelController)ScenesController.Instance.currLevelController;
			_mainMenuButton.ButtonUp += () => ScenesController.Instance.currLevelController.ChangeScene(LevelController.LevelScene.MAIN_MENU, null, true);
			_mainMenuButton.ButtonDown += _UIController._OnButtonDown;
			_playAgainButton.ButtonUp += () => ScenesController.Instance.currLevelController.ChangeScene(LevelController.LevelScene.CLASSIC_MODE, levelController.levelSettings, true);
			_playAgainButton.ButtonDown += _UIController._OnButtonDown;
		}
		


    }
    public void SetUpVictoryUI(BasePlayerController winner){
		_winnerText.Text = $"{winner.PlayerName} wins";
		
		List<string> defeatedPlayers = new();
		List<string> takenFateText = new();
		// Set fates title
		foreach (BasePlayerController player in GlobalHelper.Instance.GameController.players)
		{
			if(player != winner){
				defeatedPlayers.Add(player.PlayerName);
			}

			VBoxContainer fateContainer = _fateContainerScene.Instantiate() as VBoxContainer;
			_fatesContainer.AddChild(fateContainer);
			if(player == winner){
				_fatesContainer.MoveChild(fateContainer, 1);
			}
			Label playerNameLabel = fateContainer.GetNode("player_name") as Label;
			playerNameLabel.Text = player.PlayerName;
			if((int)player.playerStats.GetStats()["goats_clicked"] >= 30){
				playerNameLabel.Text = player.PlayerName + ", the Goat Lover";
			}
			Label playerFateLabel = fateContainer.GetNode("player_fate") as Label;
			// Get fate
			Fate.Luck luck = Fate.GetLuckFromLuckyScore((float)player.playerStats.GetStats()["lucky_score"]);
			string fateText = GlobalHelper.Instance.RandomFates[luck][RandomGenerator.Instance.GetRandIntInRange(0, GlobalHelper.Instance.RandomFates[luck].Count - 1)].Text;
			while(takenFateText.Contains(fateText))
			{
				fateText = GlobalHelper.Instance.RandomFates[luck][RandomGenerator.Instance.GetRandIntInRange(0, GlobalHelper.Instance.RandomFates[luck].Count - 1)].Text;
			}
			takenFateText.Add(fateText);
			playerFateLabel.Text = fateText;
		}

		// SET QUOTE
		string quote = GlobalHelper.Instance.RandomQuotes[RandomGenerator.Instance.GetRandIntInRange(0, GlobalHelper.Instance.RandomQuotes.Count - 1)];

		// If the quote is dynamic
		quote = quote.Replace("{winner_player}", winner.PlayerName);
		string defeatedPlayersString = defeatedPlayers[0];
		if(defeatedPlayers.Count > 1){
			for (int i = 1; i < defeatedPlayers.Count; i++)
			{
				defeatedPlayersString += $" and {defeatedPlayers[i]}";
			}
		}
		quote = quote.Replace("{defeated_players}", defeatedPlayersString);

		// If it is a complaint
		List<string> playerNames = new();
		foreach (BasePlayerController player in GlobalHelper.Instance.GameController.players)
		{
			playerNames.Add(player.PlayerName);
		}
		if((playerNames.Contains("Nanni") || playerNames.Contains("nanni")) && (playerNames.Contains("Ea-nassir") || playerNames.Contains("Ea-nāṣir") || playerNames.Contains("Ea nāṣir") || playerNames.Contains("Ea nasir") || playerNames.Contains("ea nāṣir") || playerNames.Contains("ea nasir") || playerNames.Contains("ea-nāṣir") || playerNames.Contains("ea-nasir")|| playerNames.Contains("Ea Nāṣir") || playerNames.Contains("Ea Nasir")|| playerNames.Contains("Ea-Nāṣir") || playerNames.Contains("Ea-Nasir"))){
			quote = "What do you take me for, that you treat somebody like me with such contempt?";
		}

		_quoteText.Text = quote;
	}

	public async Task ShowVictoryPanel(bool playAnimation, bool playSound){
		if(Visible){
			return;
		}

		Visible = true;
		if(playSound){
			AudioManager.Instance.PlaySound(GlobalHelper.Instance.GameController.uiController.UIAudioLibrary.GetSound("victory_melody"));
		}
		
		if(playAnimation)
		{
			string animation = _anim.GetAnimationList()[1];
			
			_anim.Play(animation);
			await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
		}
		_viewStatsButton.Disabled = false;
		_mainMenuButton.Disabled = false;
		_playAgainButton.Disabled = false;
	}
	public async Task HideVictoryPanel(bool playAnimation){
		if(!Visible){
			return;
		}

		_viewStatsButton.Disabled = true;
		_mainMenuButton.Disabled = true;
		_playAgainButton.Disabled = true;

		if(playAnimation){
			string animation = _anim.GetAnimationList()[1];
			_anim.PlayBackwards(animation);
			await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
		}

		Visible = false;
	}

	private void _OnViewStatsPressed(){
		_ = _UIController.OpenStatsUI();
	}

}
