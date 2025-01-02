using Godot;
using System;
using System.Collections.Generic;

public class PlayerStats
{
    public BasePlayerController player;
    private List<int> _rolls;
    private int _enemyPiecesHit;
    private int _totalDistanceMoved; // yet to be implemented
    private int _piecesHit;
    private int _turnsSkipped;
    private int _deliveredPieces;
    public PlayerStats(BasePlayerController player){
        this.player = player;
        ReadyPlayerStats();
    }

    private void ReadyPlayerStats(){
        _rolls = new();
        _enemyPiecesHit = 0;

        player.DiceRolled += _OnRoll;
        player.EnemyPieceHit += _OnEnemeyPieceHit;
        player.PieceHit += _OnPieceHit;
        player.PieceMoved += _OnPieceMoved;
        player.TurnSkipped += _OnTurnSkipped;
        player.PieceDelivered += _OnPieceDelivered;
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
    public Dictionary<string, int> GetStats(){ // yet to be implemented
        Dictionary<string, int> stats = new();

        return stats;
    }
}