using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerStats
{
    public BasePlayerController player;
    private int _totalTurns;
    private List<int> _rolls;
    private List<float> _luckyScores;
    private int _enemyPiecesHit;
    private int _totalDistanceMoved;
    private int _piecesHit;
    private int _turnsSkipped;
    private int _deliveredPieces;
    public PlayerStats(BasePlayerController player){
        this.player = player;
        ReadyPlayerStats();
    }

    private void ReadyPlayerStats(){
        _totalTurns = 0;
        _rolls = new();
        _enemyPiecesHit = 0;
        _piecesHit = 0;
        _totalDistanceMoved = 0;
        _turnsSkipped = 0;
        _deliveredPieces = 0;
        _luckyScores = new();

        player.TurnEnded += _OnTurnEnded;
        player.DiceRolled += _OnRoll;
        player.EnemyPieceHit += _OnEnemeyPieceHit;
        player.PieceHit += _OnPieceHit;
        player.PieceMoved += _OnPieceMoved;
        player.TurnSkipped += _OnTurnSkipped;
        player.PieceDelivered += _OnPieceDelivered;
        player.LuckEventFired += _OnLuckEventFired;
    }
    private void _OnTurnEnded(){
        _totalTurns += 1;
    }
    private void _OnRoll(int roll){
        _rolls.Add(roll);
    }
    private void _OnEnemeyPieceHit(){
        _enemyPiecesHit += 1;
    }
    private void _OnPieceMoved(int dist){
        _totalDistanceMoved += dist;
    }
    private void _OnPieceHit(){
        _piecesHit += 1;
    }
    private void _OnTurnSkipped(){
        _turnsSkipped += 1;
    }
    private void _OnPieceDelivered(){
        _deliveredPieces += 1;
    }
    private void _OnLuckEventFired(float score){
        _luckyScores.Add(score);
    }
    public Dictionary<string, object> GetStats(){
        Dictionary<string, object> stats = new()
        {
            { "player_name", player.PlayerName },
            { "total_turns", _totalTurns},
            { "roll_settings", player.rollSettings },
            { "pieces_delivered", _deliveredPieces },
            { "rolls", _rolls },
            { "distance_moved", _totalDistanceMoved },
            { "enemy_pieces_hit", _enemyPiecesHit },
            { "pieces_hit", _piecesHit },
            { "turns_skipped", _turnsSkipped },
            { "lucky_score", _luckyScores.Sum() }
        };
        return stats;
    }
}