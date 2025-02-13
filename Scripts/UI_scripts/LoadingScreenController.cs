using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class LoadingScreenController : Control
{
	[Export] public int minimumLoadingScreenTimeInMilliSeconds = 1500;
	[Export] private Timer _loadingScreenTimer;
	[Export] private float _secondsBetweenLoadingScreenSwitch = 10f;
	[Export] private TextureRect _loadingScreenImage;
	[Export] private Label _loadingScreenSubtitle;
	private List<LoadingScreenData> _loadingScreens = new();
	public override void _Ready()
	{
		_loadingScreens = LoadLoadingScreens();
		_loadingScreenTimer.Timeout += _OnLoadingScreenTimerTimeout;
		HideLoadingScreen();
	}
	private List<LoadingScreenData> LoadLoadingScreens(){
		return JSONLoader.Instance.DeserializeJSONElement<List<LoadingScreenData>>("loading_screens_data", "loading_screens");
	}

	public void ShowLoadingScreen(){
		Visible = true;
		_loadingScreenTimer.Start();
		int randomIndex = RandomGenerator.Instance.GetRandIntInRange(0, _loadingScreens.Count - 1);
		_loadingScreenImage.Texture = GD.Load<Texture2D>("res://Assets/UI/loading_screens/" + _loadingScreens[randomIndex].Image);
		_loadingScreenSubtitle.Text = _loadingScreens[randomIndex].Subtitle;
	}
	public void HideLoadingScreen(){
		Visible = false;
		_loadingScreenTimer.Stop();
		_loadingScreenTimer.WaitTime = _secondsBetweenLoadingScreenSwitch;
	}
	private void _OnLoadingScreenTimerTimeout(){
		ShowLoadingScreen();
	}

	
}
