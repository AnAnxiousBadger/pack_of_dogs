using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class BasePlayerController : Node
{
    // EXPORTS
    [Export] public RollSettings rollSettings;
    // REFERENCES
    public GameController gameController;
    // OTHER
    public int playerIndex = -1;
    private string _playerName = "";
	public string PlayerName {
		get { return _playerName; }
		set { 
			_playerName = value;
			gameController.ChangeTurnDisplaName();
		}
	}
    public int roll = -1;
    public int piecesToDeliver = 0;
    private int _deliveredPieces = 0;
    public int DeliveredPieces {
        get { return _deliveredPieces; }
        set { 
            _deliveredPieces = value;
            EmitSignal(SignalName.PieceDelivered);
            gameController.boardController.SetplayerScoreLabel(this);
            if(_deliveredPieces == piecesToDeliver){
                gameController.EndGame();
            }
        }
    }
    // TURN STATES
    protected PlayerTurnBaseState _currTurnState;
    public List<PlayerTurnBaseState> turnStates;
    private List<PlayerTurnBaseState> _previousStates;

    // SIGNALS
    [Signal] public delegate void DiceRolledEventHandler(int roll);
    [Signal] public delegate void EnemyPieceHitEventHandler();
    [Signal] public delegate void PieceHitEventHandler();
    [Signal] public delegate void PieceMovedEventHandler(int dist); // yet to be implemented
    [Signal] public delegate void TurnSkippedEventHandler();
    [Signal] public delegate void PieceDeliveredEventHandler();
    
    public void ReadyPlayer(){
        if(PlayerName == ""){
			PlayerName = Name;
		}
        _deliveredPieces = 0;
    }
    public void SwitchToNextTurnState(){
        if(_currTurnState != null){
            _currTurnState.ExitTurnState();
            _previousStates.Add(_currTurnState);
        }
        
        if(turnStates.Count > 0){
            _currTurnState = turnStates[0];
            turnStates.RemoveAt(0);
            _currTurnState.EnterTurnState();

        }
        else{
            gameController.SwitchTurn();
        }
    }

    public void SwitchToPreviousTurnState(){
        _currTurnState?.ExitTurnState();
        turnStates.Insert(0, _currTurnState);
        _currTurnState = _previousStates.Last();
        _previousStates.RemoveAt(_previousStates.Count - 1);
        _currTurnState.EnterTurnState();
    }

    public virtual void StartTurn(){
        turnStates = new();
        _previousStates = new();
    }
    public abstract void EndTurn();
    public abstract void ProcessTurn(float delta);
    public abstract void AddTurnToStateQueue();

}