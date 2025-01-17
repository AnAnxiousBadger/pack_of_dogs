using Godot;
using System;
using System.Collections.Generic;

public class RealPlayerTurnRollState : PlayerTurnBaseState
{
    private new RealPlayerController p;
    private List<PlayerRollScore> playerRollScores;
    public override void EnterTurnState()
    {
        GameController.Instance.diceController.CanRoll = true;
        GameController.Instance.diceController.DiceRolled += _OnDiceRolled;
        playerRollScores = p.CalculateLuckyRolls();
    }

    public override void ExitTurnState()
    {
        GameController.Instance.diceController.DiceRolled -= _OnDiceRolled;
    }

    public override void ProcessTurnState(float delta){}

    private void _OnDiceRolled(int roll){
        p.roll = roll;
        p.EmitSignal(BasePlayerController.SignalName.DiceRolled, roll);
        for (int i = 0; i < playerRollScores.Count; i++)
        {
            PlayerRollScore playerRollScore = playerRollScores[i];
            if(playerRollScore.IsAffected){
                playerRollScore.Player.EmitSignal(BasePlayerController.SignalName.LuckEventFired, playerRollScore.GetRollScore(roll));
            }
        }
        // SET SKIP IF CANNOT MOVE
        if(roll == 0){
            GameController.Instance.ChangeSkipButtonActivity(true);
        }
        else{
            int noMovePicesCount = 0;
            int piecesInGameCount = 0;
            for (int i = 0; i < GameController.Instance.boardController.pieces.Count; i++)
            {
                PieceController piece = GameController.Instance.boardController.pieces[i];
                if(piece.playerIndex == p.playerIndex && !piece.hasArrived){
                    piecesInGameCount ++;
                    List<BoardNodeController> possibleDestination = piece.currNode.MoveAlongNodesFromNode(roll, p.playerIndex, false);
                    if(possibleDestination.Count == 0){
                        noMovePicesCount++;
                    }
                }
            }
            if(noMovePicesCount == piecesInGameCount){
                GameController.Instance.ChangeSkipButtonActivity(true);
            }
        }

        // SWITCH TO SELECT PIECE STATE
        p.SwitchToNextTurnState();
    }

    public RealPlayerTurnRollState(RealPlayerController p) : base(p){
        this.p = p;
    }
}