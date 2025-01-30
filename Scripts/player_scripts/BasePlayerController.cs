using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public abstract partial class BasePlayerController : Node
{
    // EXPORTS
    [Export] public bool isActive = true;
    [Export] public RollSettings rollSettings;
    // REFERENCES
    // OTHER
    public int playerIndex = -1;
    [Export] private string _playerName = "";
	public string PlayerName {
		get { return _playerName; }
		set { 
			_playerName = value;
			GameController.Instance.ChangeTurnDisplaName();
		}
	}
    public PlayerStats playerStats;
    public int roll = -1;
    public List<PieceController> pieces = new();
    public int piecesToDeliver = 0;
    private int _deliveredPieces = 0;
    public int DeliveredPieces {
        get { return _deliveredPieces; }
        set { 
            _deliveredPieces = value;
            EmitSignal(SignalName.PieceDelivered);
            if(_deliveredPieces == piecesToDeliver){
                GameController.Instance.EndGame(this);
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
    [Signal] public delegate void PieceMovedEventHandler(int dist);
    [Signal] public delegate void TurnSkippedEventHandler();
    [Signal] public delegate void PieceDeliveredEventHandler();
    [Signal] public delegate void LuckEventFiredEventHandler(float score);
    [Signal] public delegate void TurnStartedEventHandler();
    [Signal] public delegate void TurnEndedEventHandler();
    
    public void ReadyPlayer(){
        if(PlayerName == ""){
			PlayerName = Name;
		}
        _deliveredPieces = 0;
        playerStats = new(this);
    }
    public void SwitchToNextTurnState(){
        if(_currTurnState != null){
            _currTurnState.ExitTurnState();
            _previousStates.Add(_currTurnState);
            _currTurnState = null;
        }
        
        if(turnStates.Count > 0){
            _currTurnState = turnStates[0];
            turnStates.RemoveAt(0);
            _currTurnState.EnterTurnState();

        }
        else{
            GameController.Instance.SwitchTurn();
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
        EmitSignal(SignalName.TurnStarted);
    }
    public virtual void EndTurn(){
        EmitSignal(SignalName.TurnEnded);
        _currTurnState?.ExitTurnState();
    }
    public abstract void ProcessTurn(float delta);
    public abstract void AddTurnToStateQueue();
    /// <summary>
    /// Returns wheter the the player has to skip or not
    /// </summary>
    public bool GetSkippingAvailability(int roll){
        if(roll == 0){
            return true;
        }
        else{
            int piecesCannotMoveCount = 0;
            int piecesInGameCount = 0;
            foreach (PieceController piece in pieces)
            {
                if(!piece.hasArrived){
                    piecesInGameCount ++;
                    if(!piece.CanPieceMove(roll)){
                        piecesCannotMoveCount ++;
                    }
                }
            }
            if(piecesCannotMoveCount == piecesInGameCount){
                return true;
            }
            else{
                return false;
            }
        }
    }

    public List<PlayerRollScore> CalculateRollLuckScores(){
        // How much luck score each roll worths
        Godot.Collections.Dictionary<int, float> luckyScoreForRolls = new();
        // List of rolls that might hurt enemies
        List<(BasePlayerController, int)> badRollsForEnemies = new(); // <player, roll>

        List<int> rollQualities = new(); // -1 - bad roll, 0 - neutral roll, 1 - good roll
        int totalGoodRollsWeigth = 0;
        int totalBadRollsWeigth = 0;
        for (int i = 0; i < rollSettings.rollChances.Length; i++)
        {
            int roll = rollSettings.rollChances[i].result;
            int rollQuality;
            if(roll == 0){
                rollQuality = -1;
            }
            else{
                List<int> rollQualitiesForPieces = new();
                for (int j = 0; j < pieces.Count; j++)
                {
                    if(!pieces[j].hasArrived){
                        int rollQualityForPiece = 0;
                        //List<BoardNodeController> destinations = pieces[j].currNode.MoveForwardAlongNodesFromNode(roll, playerIndex, false);
                        List<BoardNodeController> destinations = GameController.Instance.boardController.MoveForwardAlongNodesFromNode(pieces[j].currNode, roll, playerIndex, false);
                        if(destinations.Count == 0) {
                            rollQualityForPiece = -1;
                        }
                        else{
                            for (int k = 0; k < destinations.Count; k++)
                            {
                                if(destinations[k].HasModifier("double_turn_modifier")){
                                    rollQualityForPiece = 1;
                                }
                                if(destinations[k].GetEnemyPieces(this).Count > 0 /*!= null*/){
                                    rollQualityForPiece = 1;
                                    // Add roll that affects enemies
                                    //PieceController enemyPieceOnNode = destinations[k].GetEnemyPieces(this);
                                    List<PieceController> enemyPiecesOnNode = destinations[k].GetEnemyPieces(this);
                                    List<BasePlayerController> playersAlreadyCounted = new();
                                    foreach (PieceController enemyPiece in enemyPiecesOnNode)
                                    {   
                                        if(!playersAlreadyCounted.Contains(enemyPiece.player)){
                                            playersAlreadyCounted.Add(enemyPiece.player);
                                            badRollsForEnemies.Add((enemyPiece.player, roll));
                                        }
                                    }
                                    
                                }
                            }
                        }
                        rollQualitiesForPieces.Add(rollQualityForPiece);
                    }
                }
                rollQuality = rollQualitiesForPieces.Max();
            }
            if(rollQuality == 1){
                totalGoodRollsWeigth += rollSettings.rollChances[i].weight;
            }
            else if(rollQuality == -1){
                totalBadRollsWeigth += rollSettings.rollChances[i].weight;
            }
            rollQualities.Add(rollQuality);
        }

        List<PlayerRollScore> playerRollScores = new();
        for (int i = 0; i < GameController.Instance.players.Count; i++)
        {
            playerRollScores.Add(new PlayerRollScore(GameController.Instance.players[i], rollSettings));
        }

        for (int i = 0; i < rollSettings.rollChances.Length; i++)
        {
            RollScore rollScore;
            if(rollQualities[i] == 1){
                rollScore = new(rollSettings.rollChances[i].result, 1f / totalGoodRollsWeigth);
            }
            else if(rollQualities[i] == -1){
                rollScore = new(rollSettings.rollChances[i].result, - 1f / totalBadRollsWeigth);
            }
            else{
                rollScore = new(rollSettings.rollChances[i].result, 0f);
            }
            playerRollScores.Find(p => p.Player == this).SetRollScore(rollScore);
        }



        // CALCULATIONS FOR ENEMIES
        Dictionary<BasePlayerController, List<int>> enemiesRollsDict = new(); // <player, unlucky rolls>
        for (int i = 0; i < GameController.Instance.players.Count; i++)
        {
            if(GameController.Instance.players[i] != this){
                enemiesRollsDict.Add(GameController.Instance.players[i], new List<int>());

            }  
        }
        // Pair enemy players and bad rolls for them
        for (int i = 0; i < badRollsForEnemies.Count; i++)
        {
            if(!enemiesRollsDict[badRollsForEnemies[i].Item1].Contains(badRollsForEnemies[i].Item2)){
                enemiesRollsDict[badRollsForEnemies[i].Item1].Add(badRollsForEnemies[i].Item2);
            }
        }


        for (int i = 0; i < GameController.Instance.players.Count; i++)
        {
            BasePlayerController p = GameController.Instance.players[i];
            if(p != this){
                PlayerRollScore playerRollScore = playerRollScores.Find(pl => pl.Player == p);

                int totalBadWeights = 0;
                int totalGoodweights = 0;

                for (int j = 0; j < rollSettings.rollChances.Length; j++)
                {
                    RollChance rollChance = rollSettings.rollChances[j];

                    if(enemiesRollsDict[p].Count > 0){
                        if(enemiesRollsDict[p].Contains(rollChance.result)){
                            totalBadWeights += rollChance.weight;
                        }
                        else{
                            totalGoodweights += rollChance.weight;
                        }
                    }
                    
                }

                for (int j = 0; j < rollSettings.rollChances.Length; j++)
                {
                    RollChance rollChance = rollSettings.rollChances[j];
                     if(enemiesRollsDict[p].Count > 0){
                        float score;
                        if(enemiesRollsDict[p].Contains(rollChance.result)){
                            score = - 1f / totalBadWeights;
                            
                        }
                        else{
                            score =  1f / totalGoodweights;
                        }
                        playerRollScore.SetRollScore(new RollScore(rollSettings.rollChances[j].result, score));
                    }
                    
                }
            }
        }

        return playerRollScores;
        
    }
    
}