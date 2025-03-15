using Godot;
using System;

public partial class MainMenuUIController : Control
{
	[Export] private MainMenuLevelController _mainMenuLevelController;
	[Export] private Label _versionLabel;
	[ExportGroup("Buttons")]
	[Export] private Button _classicModeButton;
	[Export] private Button _kurModeButton;
	[Export] private Button _leaveButton;
	[Export] private Button _settingsButton;

	[ExportGroup("Menu Panels")]
	[Export] private GameModePanelController _classicModePanelController;
	[Export] private GameModePanelController _kurModePanelController;
	[Export] private SettingsPanelController _settingsPanelController;


	public override void _Ready()
	{
		_versionLabel.Text = "v " + ProjectSettings.GetSetting("application/config/version").ToString();

		_classicModeButton.Toggled += _OnClassicModeButtonToggled;
		_kurModeButton.Toggled += _OnKurModeButtonToggled;
		_leaveButton.ButtonUp += _OnLeaveButtonUp;
		_settingsButton.Toggled += _OnSettingsButtonToggled;


		_classicModePanelController.HidePanel();
		_kurModePanelController.HidePanel();
		_settingsPanelController.HidePanel();
	}

	private void _OnClassicModeButtonToggled(bool toggled)
	{
		if (toggled)
		{
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
			// Listened to by MainMenuLevelController
			_classicModePanelController.EmitSignal(GameModePanelController.SignalName.GameModeClicked);
			_classicModePanelController.ShowPanel();
			_kurModePanelController.HidePanel();
			_settingsPanelController.HidePanel();
			_kurModeButton.ButtonPressed = false;
			_settingsButton.ButtonPressed = false;
		}
		else
		{
			_classicModePanelController.HidePanel();
		}
	}

	private void _OnKurModeButtonToggled(bool toggled)
	{
		if (toggled)
		{
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
			_kurModePanelController.EmitSignal(GameModePanelController.SignalName.GameModeClicked);
			_kurModePanelController.ShowPanel();
			_classicModePanelController.HidePanel();
			_settingsPanelController.HidePanel();
			_classicModeButton.ButtonPressed = false;
			_settingsButton.ButtonPressed = false;
		}
		else
		{
			_kurModePanelController.HidePanel();
		}
	}

	private void _OnLeaveButtonUp()
	{
		AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
		GetTree().Quit();
	}

	private void _OnSettingsButtonToggled(bool toggled)
	{
		if (toggled)
		{
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
			_settingsPanelController.ShowPanel();
			_classicModePanelController.HidePanel();
			_kurModePanelController.HidePanel();
			_classicModeButton.ButtonPressed = false;
			_kurModeButton.ButtonPressed = false;
		}
		else
		{
			_settingsPanelController.HidePanel();
		}
	}


}
