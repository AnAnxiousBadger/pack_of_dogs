using Godot;
using System;

public partial class MainMenuUIController : Control
{
	[Export] private MainMenuLevelController _mainMenuLevelController;
	[ExportGroup("Buttons")]
	[Export] private Button _classicModeButton;
	[Export] private Button _kurModeButton;
	[Export] private Button _leaveButton;

	[ExportGroup("Gamemode Panels")]
	[Export] private GameModePanelController _classicModePanelController;
	[Export] private GameModePanelController _kurModePanelController;


    public override void _Ready()
    {
        _classicModeButton.Toggled += _OnClassicModeButtonToggled;
		_kurModeButton.Toggled += _OnKurModeButtonToggled;
		_leaveButton.ButtonUp += _OnLeaveButtonUp;

		_classicModePanelController.HidePanel();
		_kurModePanelController.HidePanel();
    }

	private void _OnClassicModeButtonToggled(bool toggled){
		if(toggled){
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
			// Listened to by MainMenuLevelController
			_classicModePanelController.EmitSignal(GameModePanelController.SignalName.GameModeClicked);	
			_classicModePanelController.ShowPanel();
			_kurModePanelController.HidePanel();
			_kurModeButton.ButtonPressed = false;
		}
	}

	private void _OnKurModeButtonToggled(bool toggled){
		if(toggled){
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
			_kurModePanelController.EmitSignal(GameModePanelController.SignalName.GameModeClicked);	
			_kurModePanelController.ShowPanel();
			_classicModePanelController.HidePanel();
			_classicModeButton.ButtonPressed = false;
		}
	}

	private void _OnLeaveButtonUp(){
		AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("menu_click"));
		GetTree().Quit();
	}


}
