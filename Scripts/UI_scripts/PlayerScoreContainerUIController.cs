using Godot;
using System;
using System.Dynamic;

public partial class PlayerScoreContainerUIController : PanelContainer
{
	private BasePlayerController _player;
	[Export] private Label _playerNameLabel;
	[Export] private Label _playerScoreLabel;
	[Export] private Label _playerMaxScoreLabel;
	[Export] private TextureRect _AIIcon;
	[Export] private TextureRect _pieceColorIcon;

	public void SetPlayerScoreLabel(BasePlayerController player){
		_playerNameLabel.Text = player.PlayerName;
		_playerScoreLabel.Text = player.DeliveredPieces.ToString();
		_playerMaxScoreLabel.Text = player.piecesToDeliver.ToString();
		_player = player;
		_player.PieceDelivered += UpdatePlayerScore;
		if(player is AIPlayerController){
			_AIIcon.Modulate = new Color(1f, 1f, 1f, 1f);
		}
		else{
			_AIIcon.Modulate = new Color(1f, 1f, 1f, 0f);
		}
		_pieceColorIcon.Modulate = GlobalHelper.Instance.GameController.playerPieceColors[player.playerIndex];
	}
	public void UpdatePlayerScore(){
		_playerScoreLabel.Text = _player.DeliveredPieces.ToString();
	}

}
