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