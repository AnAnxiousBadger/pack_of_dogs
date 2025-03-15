using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class GameModePanelController : PanelContainer
{
	[Export] private MainMenuLevelController _mainMenuLevelController;
	[Export] private Button _playButton;
	[Export] private PlayerSetupPanelController[] _playersSetupPanelControllers;
	[Signal] public delegate void GameModeSelectedEventHandler(Godot.Collections.Dictionary<string, string> data);
	[Signal] public delegate void GameModeClickedEventHandler();

	public override void _Ready()
	{
		if (_playButton != null)
		{
			_playButton.ButtonUp += _OnPlayButtonUp;
			_playButton.ButtonDown += () => AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
		}
	}
	public void ShowPanel()
	{
		Visible = true;
	}
	public void HidePanel()
	{
		Visible = false;
	}
	private void _OnPlayButtonUp()
	{
		if (Visible)
		{
			Godot.Collections.Dictionary<string, string> playerSettings = new();
			bool error = false;
			playerSettings.Add("player_num", $"{_playersSetupPanelControllers.Length}");
			for (int i = 0; i < _playersSetupPanelControllers.Length; i++)
			{
				if (_playersSetupPanelControllers[i].GetPlayerData(out Dictionary<string, string> playerDict))
				{
					playerSettings.Add($"player_{i}_name", playerDict["player_name"]);
					playerSettings.Add($"player_{i}_type", playerDict["player_type"]);
				}
				else
				{
					error = true;
				}
			}
			// Signal is detected by MainMenuLevelController
			if (!error)
			{
				EmitSignal(SignalName.GameModeSelected, playerSettings);
			}
		}
	}



}
