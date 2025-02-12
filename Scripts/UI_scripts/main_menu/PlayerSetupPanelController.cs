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
	[Export] private string generatedJSONPath;

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
		_inputLine.Text = GetRandomName();
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


	public class KingList
	{
		public List<King> Kings { get; set; } = new();
	}
	public class King
	{
		public string Name { get; set; }
	}
	private string GetRandomName(){
		string path;
		if(OS.HasFeature("editor")){
			path = ProjectSettings.GlobalizePath(generatedJSONPath);
		}
		else{
			path = OS.GetExecutablePath().GetBaseDir().PathJoin(generatedJSONPath.Remove(0, 6));
		}
		string JSONText = File.ReadAllText(path);
		
		var JSONData = JsonSerializer.Deserialize<KingList>(JSONText);
		
		string randomName = JSONData.Kings[RandomGenerator.Instance.GetRandIntInRange(0, JSONData.Kings.Count - 1)].Name;
		
		return randomName;
	}

	


}
