using Godot;
using System;
using System.Collections.Generic;

public class RealPlayerTurnRollState : PlayerTurnBaseState
{
    private readonly new RealPlayerController p;
    private List<PlayerRollScore> playerRollScores;
    public override void EnterTurnState()
    {
        GlobalHelper.Instance.GameController.diceController.CanRoll = true;
        GlobalHelper.Instance.GameController.diceController.DiceRolled += _OnDiceRolled;
        playerRollScores = p.CalculateRollLuckScores();
    }

    public override void ExitTurnState()
    {
        GlobalHelper.Instance.GameController.diceController.DiceRolled -= _OnDiceRolled;
    }

    public override void ProcessTurnState(float delta){}

    private void _OnDiceRolled(int roll){
        p.roll = roll;

        
        /*List<AIMove> possibleMoves = new();
        foreach (PieceController piece in p.pieces)
        {
            if(!piece.hasArrived){
                List<BoardNodeController> piecePossibleDestinations = GameController.Instance.boardController.MoveForwardAlongNodesFromNode(piece.currNode, roll, piece.playerIndex, false);
                for (int i = 0; i < piecePossibleDestinations.Count; i++)
                {
                    possibleMoves.Add(new AIMove(piece, piecePossibleDestinations[i], roll));
                }
            }
        }
        GD.Print("------------------------");*/

        p.EmitSignal(BasePlayerController.SignalName.DiceRolled, roll);
        for (int i = 0; i < playerRollScores.Count; i++)
        {
            PlayerRollScore playerRollScore = playerRollScores[i];
            if(playerRollScore.IsAffected){
                playerRollScore.Player.EmitSignal(BasePlayerController.SignalName.LuckEventFired, playerRollScore.GetRollScore(roll));
            }
        }
        // SET SKIP IF CANNOT MOVE
        if(p.GetSkippingAvailability(roll)){
            GlobalHelper.Instance.GameController.ChangeSkipButtonActivity(true);
        }

        // SWITCH TO SELECT PIECE STATE
        p.SwitchToNextTurnState();
    }

    public RealPlayerTurnRollState(RealPlayerController p) : base(p){
        this.p = p;
    }
}