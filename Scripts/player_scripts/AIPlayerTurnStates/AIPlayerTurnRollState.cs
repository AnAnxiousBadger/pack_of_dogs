using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AIPlayerTurnRollState : PlayerTurnBaseState
{
    private readonly new AIPlayerController p;
    private readonly float _rollWaitTime = 1f;
    private readonly float _skipWaitTime = 1f;
    private List<PlayerRollScore> playerRollScores;
    public override void EnterTurnState()
    {
        GlobalHelper.Instance.GameController.diceController.CanRoll = true;
        GlobalHelper.Instance.GameController.diceController.DiceRolled += _OnDiceRolled;
        playerRollScores = p.CalculateRollLuckScores();
        p.timer.WaitTime = _rollWaitTime;
        p.timer.Timeout += _OnRollTimerTimeout;
        p.timer.Start();
    }

    public override void ExitTurnState()
    {
        GlobalHelper.Instance.GameController.diceController.DiceRolled -= _OnDiceRolled;
    }

    public override void ProcessTurnState(float delta){}

    private void _OnRollTimerTimeout(){
        p.timer.Timeout -= _OnRollTimerTimeout;
        GlobalHelper.Instance.GameController.boardController.boardElementsController.EmitSignal(BoardElementsController.SignalName.OnRollDiceWithoutClicking, p);
    }

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
            p.timer.WaitTime = _skipWaitTime;
            p.timer.Timeout += _OnSkipTimerTimeout;
            p.timer.Start();
        }
        else{
            
            p.SwitchToNextTurnState();
        }        
    }

    private void _OnSkipTimerTimeout(){
        p.timer.Timeout -= _OnSkipTimerTimeout;
        GlobalHelper.Instance.GameController.boardController.boardElementsController.EmitSignal(BoardElementsController.SignalName.OnSkipWithoutClicking, p);
    }

    public AIPlayerTurnRollState(AIPlayerController p) : base(p){
        this.p = p;
    }
}