using Godot;
using System;
using System.Threading.Tasks;
public partial class EndOfGameUIController : Control
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
	[Export] private Button _mainMenuButton; // Implement
	[Export] private Button _playAgainButton; // Implement
	public override void _Ready()
    {
        _ = HideVictoryPanel(false);
		_viewStatsButton.ButtonUp += _OnViewStatsPressed;
		_viewStatsButton.ButtonDown += _UIController._OnButtonDown;

    }
    public void SetUpVictoryUI(BasePlayerController winner){
		_winnerText.Text = $"{winner.PlayerName} wins";
		// Set quote
		// Set fates title
		foreach (BasePlayerController player in GameController.Instance.players)
		{
			VBoxContainer fateContainer = _fateContainerScene.Instantiate() as VBoxContainer;
			_fatesContainer.AddChild(fateContainer);
			if(player == winner){
				_fatesContainer.MoveChild(fateContainer, 1);
			}
			Label playerNameLabel = fateContainer.GetNode("player_name") as Label;
			playerNameLabel.Text = player.PlayerName;
			Label playerFateLabel = fateContainer.GetNode("player_fate") as Label;
			// Generate fate
		}
	}

	public async Task ShowVictoryPanel(bool playAnimation, bool playSound){
		if(Visible){
			return;
		}

		Visible = true;
		if(playSound){
			AudioManager.Instance.PlaySound(GameController.Instance.uiController.UIControllerAudioLibrary.GetSound("end_melody"), GameController.Instance, false);
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
