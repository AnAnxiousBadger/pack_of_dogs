using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class GameController : Node3D
{

	// EXPORTS
	[Export] public Camera3D cam;
	[Export] public UIController uiController;
	[Export] public DiceController diceController;
	[Export] public BoardController boardController;
	[Export] private PackedScene _realPlayerControllerScene;
	[Export] private PackedScene _aiPlayerControllerScene;
	[Export] public Color[] playerPieceColors;
	// OTHERS
	public VisualEffectPoolController visualEffectPool;
	public List<BasePlayerController> players;
	private Queue<BasePlayerController> _playersQueue;
	public BasePlayerController currPlayer;
	private Vector2 mousePos = Vector2.Zero;
	private const float MOUSERAYDIST = 1000f;
	private PhysicsBody3D _physicsBodyUnderMouse = null;
	private Vector3 _physicsBodyHitPos = Vector3.Zero;
	public PhysicsBody3D PhysicsBodyUnderMouse
	{
		get { return _physicsBodyUnderMouse; }
		private set
		{
			boardController.boardElementsController.HandleTickableInterActions(_physicsBodyUnderMouse, value, _physicsBodyHitPos);
			_physicsBodyUnderMouse = value;
		}
	}
	public bool allowClicksOnTickableButtons = false;
	private DateTime _gameStartTime;
	private TimeSpan _totalGameTime;
	private bool _gameEnded = false;
	// SIGNALS
	[Signal] public delegate void OnRollButtonUsedEventHandler();
	[Signal] public delegate void OnRollButtonActivityChangeEventHandler(bool isActive);
	[Signal] public delegate void OnSkipButtonUsedEventHandler();
	[Signal] public delegate void OnSkipButtonActivityChangeEventHandler(bool isActive);
	[Signal] public delegate void GameEndedEventHandler(BasePlayerController winner);
	public override void _Ready()
	{
		GlobalHelper.Instance.GameController = this;
	}

	public override void _Process(double delta)
	{
		currPlayer?.ProcessTurn((float)delta);
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		(PhysicsBodyUnderMouse, _physicsBodyHitPos) = CastRayFromMouse();
	}
	public async Task ReadyGameAsync(Godot.Collections.Dictionary<string, string> settings)
	{
		// Adding children can only be done on main thread
		AddPlayers(settings);
		SetUpGame();
		await Task.Run(() =>
		{
			return;
		});
	}

	private void AddPlayers(Godot.Collections.Dictionary<string, string> settings)
	{

		List<BasePlayerController> addedPlayers = new();
		// Spawn players
		for (int i = 0; i < settings["player_num"].ToInt(); i++)
		{
			BasePlayerController player;
			if (settings[$"player_{i}_type"] == "real_player")
			{
				player = _realPlayerControllerScene.Instantiate() as BasePlayerController;
			}
			else
			{
				player = _aiPlayerControllerScene.Instantiate() as BasePlayerController;
			}
			player.PlayerName = settings[$"player_{i}_name"];
			player.isActive = true;
			addedPlayers.Add(player);
		}

		// Mix up player order
		List<BasePlayerController> shuffledPlayerList = addedPlayers.OrderBy(_ => RandomGenerator.Instance.GetRandInt()).ToList();
		foreach (BasePlayerController player in shuffledPlayerList)
		{
			GetNode("../players_container").AddChild(player);
		}
	}

	private void SetUpGame()
	{
		diceController.ReadyDiceController();
		SetUpPlayers();
		boardController.ReadyBoardController();
		for (int i = 0; i < players.Count; i++)
		{
			players[i].piecesToDeliver = players[i].pieces.Count;
		}
		uiController.SetUpUI();
	}

	private void SetUpPlayers()
	{
		players = new();
		_playersQueue = new();

		Godot.Collections.Array<Node> playersArray = GetNode("../players_container").GetChildren();
		for (int i = 0; i < playersArray.Count; i++)
		{
			BasePlayerController p = (BasePlayerController)playersArray[i];
			if (p.isActive)
			{
				p.playerIndex = players.Count;
				p.ReadyPlayer();
				players.Add(p);
				_playersQueue.Enqueue(p);
			}
		}
	}
	public void StartGame()
	{
		SwitchTurn();
		_gameStartTime = DateTime.UtcNow;
	}

	public void SwitchTurn()
	{
		currPlayer?.EndTurn();
		if (_playersQueue.Count > 0)
		{
			currPlayer = _playersQueue.Dequeue();
			currPlayer.StartTurn();
			_playersQueue.Enqueue(currPlayer);

			// SET UI
			ChangeTurnDisplaName();
		}
	}

	public void ChangeTurnDisplaName()
	{
		if (currPlayer != null)
		{
			uiController.SetTurnLabel(currPlayer.PlayerName);
		}
	}

	public (PhysicsBody3D, Vector3) CastRayFromMouse()
	{
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

		Godot.Collections.Dictionary result = space.IntersectRay(rayParams);
		if (result.ContainsKey("collider"))
		{
			resultBody = (PhysicsBody3D)result["collider"];
		}
		if (result.ContainsKey("position"))
		{
			resultCoord = (Vector3)result["position"];
		}

		return (resultBody, resultCoord);
	}

	public void EndGame(BasePlayerController winner)
	{
		_playersQueue.Clear();
		currPlayer = null;
		_gameEnded = true;
		_totalGameTime = DateTime.UtcNow - _gameStartTime;
		EmitSignal(SignalName.GameEnded, winner);
	}
	public TimeSpan GetElapsedGameTime()
	{
		if (_gameEnded)
		{
			return _totalGameTime;
		}
		else
		{
			return DateTime.UtcNow - _gameStartTime;
		}

	}

	public void RollButtonUsed()
	{
		EmitSignal(SignalName.OnRollButtonUsed);
	}
	public void ChangeRollButtonActivity(bool isActive)
	{
		EmitSignal(SignalName.OnRollButtonActivityChange, isActive);
	}
	public void SkipButtonUsed()
	{
		EmitSignal(SignalName.OnSkipButtonUsed);
	}
	public void ChangeSkipButtonActivity(bool isActive)
	{
		EmitSignal(SignalName.OnSkipButtonActivityChange, isActive);
	}
}
