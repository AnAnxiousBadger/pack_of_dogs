using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameController : Node3D
{
	// EXPORTS
	[Export] public Camera3D cam;
	[Export] public UIController uiController;
	[Export] public DiceController diceController;
	[Export] public BoardController boardController;
	[Export] public BoardElementsController boardElementsController;
	public List<BasePlayerController> players;
	private Queue<BasePlayerController> _playersQueue;
	public BasePlayerController currPlayer;
	private Vector2 mousePos = Vector2.Zero;
	public enum CollisionMask {NODE, PIECE}
	public CollisionMask _collisionMask = CollisionMask.PIECE;
	private const float MOUSERAYDIST = 1000f;
	private StaticBody3D _staticBodyUnderMouse = null;
	private Vector3 _staticBodyHitPos = Vector3.Zero;
	public StaticBody3D StaticBodyUnderMouse {
		get { return _staticBodyUnderMouse; }
		private set {
			if(value != _staticBodyUnderMouse){
				if(_staticBodyUnderMouse is TickableController t1){
					t1.OnHoverStopped();
				}
				_staticBodyUnderMouse = value;
				if(value is TickableController t2){
					t2.OnHover();
				}
			}
			if(value is TickableController t3){
				boardElementsController.HandleClickTickable(t3, _staticBodyHitPos);
			}
		}
	}
	public override void _Ready()
	{
		SetUpGame();
		SwitchTurn();
	}

	public override void _Process(double delta)
    {
		(StaticBodyUnderMouse, _staticBodyHitPos) = CastRayFromMouse();
        currPlayer?.ProcessTurn((float)delta);
    }

	private void SetUpGame(){
		uiController.ReadyUIController();
		diceController.ReadyDiceController(this);
		SetUpPlayers();
		boardController.ReadyBoardController(this);
		for (int i = 0; i < players.Count; i++)
		{
			players[i].piecesToDeliver = 1;
		}
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

    public void SwitchTurn(){
		currPlayer?.EndTurn();
		if(_playersQueue.Count > 0){
			currPlayer = _playersQueue.Dequeue();
			currPlayer.StartTurn();
			_playersQueue.Enqueue(currPlayer);

			// SET UI
			ChangeTurnDisplaName();
			uiController.SetDiceRollLabelUnset();
		}
	}

	public void ChangeTurnDisplaName(){
		if(currPlayer != null){
			uiController.SetTurnLabel(currPlayer.PlayerName);
		}
	}

	public (StaticBody3D, Vector3) CastRayFromMouse(){
        StaticBody3D resultBody = null;
		Vector3 resultCoord = Vector3.Zero;
        Vector2 mouse = GetViewport().GetMousePosition();
		PhysicsDirectSpaceState3D space = GetWorld3D().DirectSpaceState;
		Vector3 start = GetViewport().GetCamera3D().ProjectRayOrigin(mouse);
		Vector3 end = GetViewport().GetCamera3D().ProjectPosition(mouse, MOUSERAYDIST);
		PhysicsRayQueryParameters3D rayParams = new()
		{
			From = start,
			To = end
		};

		if(_collisionMask == CollisionMask.NODE){
			rayParams.CollisionMask = 0b00000000_00000000_00000000_00000101;
		}
		else if(_collisionMask == CollisionMask.PIECE){
			rayParams.CollisionMask = 0b00000000_00000000_00000000_00000110;
		}

		Godot.Collections.Dictionary result = space.IntersectRay(rayParams);
		if(result.ContainsKey("collider")){
			resultBody = (StaticBody3D) result["collider"];
		}
		if(result.ContainsKey("position")){
			resultCoord = (Vector3) result["position"];
		}

        return (resultBody, resultCoord);
    }

	public void EndGame(){
		_playersQueue.Clear();
		GD.Print("END");
	}
}
