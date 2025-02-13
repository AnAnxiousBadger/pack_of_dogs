using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

public partial class UIController : Control
{
	// EXPORTS
	[Export] public AudioLibrary UIAudioLibrary;
	[Export] private TurnPanelUIController _turnPanel;
	[Export] private VBoxContainer _scoresPanel;
	[Export] private PackedScene _playerScorePanel;
	[Export] private StatsUIController _statsUIController;
	[Export] private VictoryUIController _victoryUI;
	[Export] private Timer _UITimer;
	[Export] private float _victoryPanelWaitTime = 1f;
	[Export] private Button _bottomRightMenuButton;
	[Export] private PanelContainer _bottomRightMenu;
	[Export] private Button _bottomRightMenuRestartButton;
	[Export] private Button _bottomRightMenuMainMenuButton;

	// OTHERS
	public enum GameUIState {GAME, VICTORY};
	private GameUIState _currGameUIState = GameUIState.GAME;

	private enum KeyInput {TAB, ESC};
	public bool canEscapeActOnUI = true;


    public override void _Process(double delta)
    {
		if(Input.IsActionJustPressed("tab")){
            HandleKeyInputs(KeyInput.TAB);
		}
		else if(Input.IsActionJustPressed("escape")){
            HandleKeyInputs(KeyInput.ESC);
		}


    }
    public void SetUpUI(){
		for (int i = 0; i < GlobalHelper.Instance.GameController.players.Count; i++)
		{
			PlayerScoreContainerUIController scoreContainer = _playerScorePanel.Instantiate() as PlayerScoreContainerUIController;
			_scoresPanel.AddChild(scoreContainer);
			scoreContainer.SetPlayerScoreLabel(GlobalHelper.Instance.GameController.players[i]);
		}
		GlobalHelper.Instance.GameController.GameEnded += SetUpVictoryUI;
		_bottomRightMenuButton.ButtonUp +=_OnBottomRightMenuButtonUp;
		_bottomRightMenuButton.ButtonDown += _OnButtonDown;

		// Needed check to run unique scene
		if(ScenesController.Instance != null){
			ClassicModeLevelController levelController = (ClassicModeLevelController)ScenesController.Instance.currLevelController;
			_bottomRightMenuRestartButton.ButtonUp += () => ScenesController.Instance.currLevelController.ChangeScene(LevelController.LevelScene.CLASSIC_MODE, levelController.levelSettings, true);
			_bottomRightMenuRestartButton.ButtonDown += _OnButtonDown;
			_bottomRightMenuMainMenuButton.ButtonUp += () => ScenesController.Instance.currLevelController.ChangeScene(LevelController.LevelScene.MAIN_MENU, null, true);
			_bottomRightMenuMainMenuButton.ButtonDown += _OnButtonDown;
		}

		_victoryUI.SetUpVictoryUI();

		_bottomRightMenu.Visible = false;
	}
    public void SetTurnLabel(string playerName){
		_turnPanel.ChangeTurn(playerName);
	}

	private void SetUpVictoryUI(BasePlayerController winner){
		_victoryUI.SetUpVictoryUI(winner);
		_UITimer.WaitTime = _victoryPanelWaitTime;
		_UITimer.Timeout += _OnVictoryPanelTimerTimeout;
		_UITimer.Start();
	}
	private void _OnVictoryPanelTimerTimeout(){
		_UITimer.Timeout -= _OnVictoryPanelTimerTimeout;
		// Hide all other panels
		_statsUIController.SetStatTableVisibility(false);
		_scoresPanel.Visible = false;
		_turnPanel.Visible = false;
		_bottomRightMenu.Visible = false;
		// Show Victory panel
		_currGameUIState = GameUIState.VICTORY;
		_ = _victoryUI.ShowVictoryPanel(true, true);
	}

	private void HandleKeyInputs(KeyInput input){
		switch (input)
		{
			case KeyInput.TAB:
				_bottomRightMenu.Visible = false;
				if(!_statsUIController.Visible){
					PlayMenuClickSound();
					_ = OpenStatsUI();
				}
				else{
					PlayMenuClickSound();
					CloseStatsUI();
				}

				break;

			case KeyInput.ESC:
				if(canEscapeActOnUI){
					if((_statsUIController.Visible && _bottomRightMenu.Visible) || (!_statsUIController.Visible && _bottomRightMenu.Visible)){
						PlayMenuClickSound();
						_bottomRightMenu.Visible = false;
					}
					else if(_statsUIController.Visible){
						PlayMenuClickSound();
						CloseStatsUI();
					}
					else{
						PlayMenuClickSound();
						_bottomRightMenu.Visible = true;
					}
				}
				break;
		}
	}

	public async Task OpenStatsUI(){
		if(_currGameUIState == GameUIState.VICTORY){
			await _victoryUI.HideVictoryPanel(true);
		}
		_statsUIController.GetAndDisplayStats();
		_statsUIController.SetStatTableVisibility(true);
	}
	private void CloseStatsUI(){
		_statsUIController.SetStatTableVisibility(false);
		if(_currGameUIState == GameUIState.VICTORY){
			_= _victoryUI.ShowVictoryPanel(true, false);
		}
	}
	public void PlayMenuClickSound(){
		AudioManager.Instance.PlaySound(UIAudioLibrary.GetSound("menu_click"));
	}
	public void _OnButtonDown(){
		PlayMenuClickSound();
	}

	private void _OnBottomRightMenuButtonUp(){
		_bottomRightMenu.Visible = !_bottomRightMenu.Visible;
	}

}
