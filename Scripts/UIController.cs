using Godot;
using System;

public partial class UIController : Control
{
	// EXPORTS
	[Export] private TurnPanelUIController _turnPanel;
	[Export] private VBoxContainer _scoresPanel;
	[Export] private PackedScene _playerScorePanel;

	public void SetUpUI(){
		for (int i = 0; i < GameController.Instance.players.Count; i++)
		{
			PlayerScoreContainerUIController scoreContainer = _playerScorePanel.Instantiate() as PlayerScoreContainerUIController;
			_scoresPanel.AddChild(scoreContainer);
			scoreContainer.SetPlayerScoreLabel(GameController.Instance.players[i]);
		}
	}
    public void SetTurnLabel(string playerName){
		_turnPanel.ChangeTurn(playerName);
	}
}
