using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class GameModePanelController : PanelContainer
{
	private bool isHovered = false;
	private bool isOpened = false;
	[Export] private LevelController.LevelScene _level;
	[Export] private Control _playersSetUpPanelMarginPanel;
	[Export] private UIAnimationComponent _animComp;
	[Export] private AnimationPlayer _anim;
	[Export] private Button _playButton;
	[Export] private PlayerSetupPanelController[] _playersSetupPanelControllers;
	[Signal] public delegate void GameModeSelectedEventHandler(Godot.Collections.Dictionary<string, string> data);
	[Signal] public delegate void GameModeClickedEventHandler();

	public override void _Ready()
	{
		this.MouseEntered += _OnMouseEntered;
		this.MouseExited += _OnMouseExited;
		if(_playButton != null){
			_playButton.ButtonUp += _OnPlayButtonUp;
		}
		

	}
    public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("left_mouse")){
			if(!isOpened && isHovered)
			{
				if(_level == LevelController.LevelScene.QUIT){
					GetTree().Quit();
					return;
				}
				DisplayPlayerSetUpPanel();
			}
		}
    }

    private void _OnMouseEntered(){
		isHovered = true;
		if(!isOpened){
			_animComp.OnHover();
			_anim.Play("hover");
		}
		
	}
	private void _OnMouseExited(){
		isHovered = false;
		if(!isOpened){
			_anim.PlayBackwards("hover");
			_animComp.OnHoverEnded();
		}
	}

	private void DisplayPlayerSetUpPanel(){	
		EmitSignal(SignalName.GameModeClicked);	
		_anim.Play("click");
		isOpened = true;
		_animComp.OnHoverEnded();
	}

	private async Task HidePlayerSetUpPanel(){
		_anim.PlayBackwards("click");
		
		_animComp.OnHoverEnded();
		await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
		isOpened = false;
	}

	private void _OnPlayButtonUp(){
		Godot.Collections.Dictionary<string, string> playerSettings = new();
		bool error = false;
		playerSettings.Add("player_num", $"{_playersSetupPanelControllers.Length}");
		for (int i = 0; i < _playersSetupPanelControllers.Length; i++)
		{
			if(_playersSetupPanelControllers[i].GetPlayerData(out Dictionary<string, string> playerDict)){
				playerSettings.Add($"player_{i}_name", playerDict["player_name"]);
				playerSettings.Add($"player_{i}_type", playerDict["player_type"]);
			}
			else{
				error = true;
			}
		}
		// Signal is detected by MainMenuLevelController
		if(!error){
			EmitSignal(SignalName.GameModeSelected, playerSettings);
		}
		
	}



}
