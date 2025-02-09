using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class GameModePanelController : PanelContainer
{
	private bool isHovered = false;
	private bool isOpened = false;
	[Export] private Control _playersSetUpPanelMarginPanel;
	[Export] private UIAnimationComponent _animComp;
	[Export] private AnimationPlayer _anim;
	[Export] private Button _playButton;
	[Export] private PlayerSetupPanelController[] _playersSetupPanelControllers;

	public override void _Ready()
	{
		this.MouseEntered += _OnMouseEntered;
		this.MouseExited += _OnMouseExited;
		_playButton.ButtonUp += _OnPlayButtonUp;

	}
    public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("left_mouse")){
			if(!isOpened && isHovered)
			{
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
		List<Dictionary<string, string>> playerSettings = new();
		foreach (PlayerSetupPanelController playerSetupPanelController in _playersSetupPanelControllers)
		{
			if(playerSetupPanelController.GetPlayerData(out Dictionary<string, string> playerDict)){
				playerSettings.Add(playerDict);
			}
		}
		if(playerSettings.Count == _playersSetupPanelControllers.Length){
			GD.Print("GO");
		}
	}



}
