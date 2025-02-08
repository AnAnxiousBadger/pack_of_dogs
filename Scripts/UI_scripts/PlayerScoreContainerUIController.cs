using Godot;
using System;
using System.Dynamic;

public partial class PlayerScoreContainerUIController : PanelContainer
{
	private BasePlayerController _player;
	[Export] private Label _playerNameLabel;
	[Export] private Label _playerScoreLabel;
	[Export] private Label _playerMaxScoreLabel;
	[Export] private int _maxNameLength = 15;
	//[Export] private PanelContainer _currentPanel;

	public void SetPlayerScoreLabel(BasePlayerController player){
		string playerName = player.PlayerName;
		if(playerName.Length > _maxNameLength){
			playerName = playerName.Substring(0, _maxNameLength - 3) + "...";
		}
		_playerNameLabel.Text = playerName;
		_playerScoreLabel.Text = player.DeliveredPieces.ToString();
		_playerMaxScoreLabel.Text = player.piecesToDeliver.ToString();
		_player = player;
		_player.PieceDelivered += UpdatePlayerScore;
		//_player.TurnStarted += ShowCurrentPlayer;
		//_player.TurnEnded += HideCurrentPlayer;
	}
	public void UpdatePlayerScore(){
		_playerScoreLabel.Text = _player.DeliveredPieces.ToString();
	}

	/*private void ShowCurrentPlayer(){
		_currentPanel.Modulate = new Color(1,1,1,1);
	}

	private void HideCurrentPlayer(){
		_currentPanel.Modulate = new Color(1,1,1,0);
	}*/

}
