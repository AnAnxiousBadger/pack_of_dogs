using Godot;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public partial class PlayerSetupPanelController : PanelContainer
{
	[Export] private MainMenuLevelController _mainMenuLevelController;
	[Export] private LineEdit _inputLine;
	[Export] private Theme _mainMenuNormalTheme;
	[Export] private Theme _mainMenuErrorTheme;
	[Export] private Button _generateRandomNameButton;
	[Export] private Button _humanPlayerButton;
	[Export] private Button _AIPlayerButton;

    public override void _Ready()
    {
        _generateRandomNameButton.ButtonUp += _OnGenerateRandomName;
		_generateRandomNameButton.ButtonDown += _OnButtonDownPlaySound;
		_humanPlayerButton.Toggled += _OnHumanPlayerButtonToggled;
		_humanPlayerButton.ButtonDown += _OnButtonDownPlaySound;
		_AIPlayerButton.Toggled += _OnAIPlayerButtonToggled;
		_AIPlayerButton.ButtonDown += _OnButtonDownPlaySound;
		_inputLine.TextChanged += _OnInputLineTextChanged;
    }
	private void _OnButtonDownPlaySound(){
		AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
	}

	private void _OnGenerateRandomName(){
		_inputLine.Text = GlobalHelper.Instance.RandomNames[RandomGenerator.Instance.GetRandIntInRange(0, GlobalHelper.Instance.RandomNames.Count - 1)];
		_inputLine.EmitSignal(LineEdit.SignalName.TextChanged, _inputLine.Text);
	}
	private void _OnHumanPlayerButtonToggled(bool isOn){
		_AIPlayerButton.ButtonPressed = !isOn;
	}
	private void _OnAIPlayerButtonToggled(bool isOn){
		_humanPlayerButton.ButtonPressed = !isOn;
		if(isOn && _inputLine.Text == String.Empty){
			_OnGenerateRandomName();
		}
		
	}
	private void _OnInputLineTextChanged(string text){
		if(text == String.Empty){
			_inputLine.Theme = _mainMenuErrorTheme;
		}
		else{
			_inputLine.Theme = _mainMenuNormalTheme;
		}
	}
	public bool GetPlayerData(out Dictionary<string, string> playerDict){
		playerDict = new();

		string playerName = _inputLine.Text;
		if(playerName == String.Empty){
			_inputLine.Theme = _mainMenuErrorTheme;
			return false;
		}
		playerDict.Add("player_name", playerName);

		string playerType = "real_player";
		if(_AIPlayerButton.ButtonPressed){
			playerType = "ai_player";
		}
		playerDict.Add("player_type", playerType);
		
		return true;
	}
}
