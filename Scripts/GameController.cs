using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameController : Node3D
{
	[Export] public Camera3D cam;
	[Export] public UIController uiController;
	[Export] public DiceController diceController;
	[Export] public BoardController boardController;
	public List<BasePlayerController> players;
	private Queue<BasePlayerController> _playersQueue;
	public BasePlayerController currPlayer;
	private int turn = 0;
	private Vector2 mousePos = Vector2.Zero;
	public override void _Ready()
	{
		SetUpGame();
		SwitchTurn();
	}

	private void SetUpGame(){
		uiController.ReadyUIController();
		diceController.ReadyDiceController(this);
		SetUpPlayers();
		boardController.ReadyBoardController(this);
	}

	private void SetUpPlayers(){
		players = new();
		_playersQueue = new();

		Godot.Collections.Array<Node> playersArray = GetNode("../players_container").GetChildren();
		for (int i = 0; i < playersArray.Count; i++)
		{
			BasePlayerController p = (BasePlayerController)playersArray[i];
			p.gameController = this;
			p.playerIndex = i;
			p.ReadyPlayer();
			players.Add(p);
			_playersQueue.Enqueue(p);
		}
	}

    public override void _Process(double delta)
    {
        currPlayer?.ProcessTurn((float)delta);
    }

    public void SwitchTurn(){
		currPlayer?.EndTurn();
		currPlayer = _playersQueue.Dequeue();
		currPlayer.StartTurn();
		_playersQueue.Enqueue(currPlayer);

		// SET UI
		ChangeTurnDisplaName();
		uiController.SetDiceRollLabelUnset();
		
	}

	public void ChangeTurnDisplaName(){
		if(currPlayer != null){
			uiController.SetTurnLabel(currPlayer.PlayerName);
		}
	}
}
