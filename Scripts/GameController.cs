using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameController : Node3D
{
	public static GameController Instance { get;  private set;}
	// EXPORTS
	[Export] public Camera3D cam;
	[Export] public UIController uiController;
	[Export] public DiceController diceController;
	[Export] public BoardController boardController;
	[Export] public VisualEffectPoolController visualEffectPool;
	public List<BasePlayerController> players;
	private Queue<BasePlayerController> _playersQueue;
	public BasePlayerController currPlayer;
	private Vector2 mousePos = Vector2.Zero;
	public enum CollisionMask {NODE, PIECE}
	public CollisionMask collisionMask = CollisionMask.PIECE;
	private const float MOUSERAYDIST = 1000f;
	private PhysicsBody3D _physicsBodyUnderMouse = null;
	private Vector3 _staticBodyHitPos = Vector3.Zero;
	public PhysicsBody3D PhysicsBodyUnderMouse {
		get { return _physicsBodyUnderMouse; }
		private set {
			boardController.boardElementsController.HandleTickableInterActions(_physicsBodyUnderMouse, value, _staticBodyHitPos);
			_physicsBodyUnderMouse = value;
		}
	}
	public bool allowClicksOnTickableButtons = false;
	// SIGNALS
	[Signal] public delegate void OnRollButtonUsedEventHandler();
	[Signal] public delegate void OnRollButtonActivityChangeEventHandler(bool isActive);
	[Signal] public delegate void OnSkipButtonUsedEventHandler();
	[Signal] public delegate void OnSkipButtonActivityChangeEventHandler(bool isActive);
	[Signal] public delegate void GameEndedEventHandler(BasePlayerController winner);
	public override void _Ready()
	{
		if(Instance != null){
			QueueFree();
			return;
		}
		Instance = this;
		SetUpGame();
		SwitchTurn();
	}

	public override void _Process(double delta)
    {
		(PhysicsBodyUnderMouse, _staticBodyHitPos) = CastRayFromMouse();
        currPlayer?.ProcessTurn((float)delta);
    }

	private void SetUpGame(){
		diceController.ReadyDiceController();
		SetUpPlayers();
		boardController.ReadyBoardController();
		for (int i = 0; i < players.Count; i++)
		{
			players[i].piecesToDeliver = players[i].pieces.Count;
		}
		uiController.SetUpUI();
	}

	private void SetUpPlayers(){
		players = new();
		_playersQueue = new();

		Godot.Collections.Array<Node> playersArray = GetNode("../players_container").GetChildren();
		for (int i = 0; i < playersArray.Count; i++)
		{
			BasePlayerController p = (BasePlayerController)playersArray[i];
			if(p.isActive){
				p.playerIndex = players.Count;
				p.ReadyPlayer();
				players.Add(p);
				_playersQueue.Enqueue(p);
			}
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
		}
	}

	public void ChangeTurnDisplaName(){
		if(currPlayer != null){
			uiController.SetTurnLabel(currPlayer.PlayerName);
		}
	}

	public (PhysicsBody3D, Vector3) CastRayFromMouse(){
        PhysicsBody3D resultBody = null;
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

		if(collisionMask == CollisionMask.NODE){
			rayParams.CollisionMask = 0b00000000_00000000_00000000_00000101;
		}
		else if(collisionMask == CollisionMask.PIECE){
			rayParams.CollisionMask = 0b00000000_00000000_00000000_00000110;
		}

		Godot.Collections.Dictionary result = space.IntersectRay(rayParams);
		if(result.ContainsKey("collider")){
			resultBody = (PhysicsBody3D) result["collider"];
		}
		if(result.ContainsKey("position")){
			resultCoord = (Vector3) result["position"];
		}

        return (resultBody, resultCoord);
    }

	public void EndGame(BasePlayerController winner){
		_playersQueue.Clear();
		currPlayer = null;
		EmitSignal(SignalName.GameEnded, winner);
		GD.Print("END");
	}

	public void RollButtonUsed(){
		EmitSignal(SignalName.OnRollButtonUsed);
	}
	public void ChangeRollButtonActivity(bool isActive){
		EmitSignal(SignalName.OnRollButtonActivityChange, isActive);
	}
	public void SkipButtonUsed(){
		EmitSignal(SignalName.OnSkipButtonUsed);
	}
	public void ChangeSkipButtonActivity(bool isActive){
		EmitSignal(SignalName.OnSkipButtonActivityChange, isActive);
	}
}
