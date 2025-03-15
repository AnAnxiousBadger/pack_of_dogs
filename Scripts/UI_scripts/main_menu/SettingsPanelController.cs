using Godot;
using System;

public partial class SettingsPanelController : PanelContainer
{
	[Export] private MainMenuLevelController _mainMenuLevelController;
	[Export] private HSlider _masterVolumeSlider;
	[Export] private CheckBox _muteCheckBox;

	public override void _Ready()
	{
		_muteCheckBox.ButtonPressed = AudioManager.Instance.MuteMusic;
		_muteCheckBox.Toggled += _OnMuteMusicToggled;

		_masterVolumeSlider.Value = Mathf.DbToLinear(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(0)) * 100);
		_masterVolumeSlider.DragEnded += _OnMasterVolumeSliderDragEnded;
	}
	public void ShowPanel()
	{
		Visible = true;
	}
	public void HidePanel()
	{
		Visible = false;
	}

	private void _OnMuteMusicToggled(bool toggled)
	{
		AudioManager.Instance.MuteMusic = toggled;
	}
	private void _OnMasterVolumeSliderDragEnded(bool valueChanged)
	{
		if (valueChanged)
		{
			AudioServer.SetBusVolumeDb(0, Mathf.LinearToDb((float)_masterVolumeSlider.Value / 100f));
			AudioManager.Instance.PlaySound(_mainMenuLevelController.UIAudioLibrary.GetSound("master_volume_check"));
		}

	}
}
