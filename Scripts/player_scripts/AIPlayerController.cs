using Godot;
using System;

public partial class AIPlayerController : BasePlayerController
{
    [Export] public Timer timer;

    // STATES
    public AIPlayerTurnRollState rollState;
    public AIPlayerTurnMovePieceState moveState;
    public override void StartTurn()
    {
        base.StartTurn();
        GameController.Instance.allowClicksOnTickableButtons = false;
        // CREATE STATES
        rollState = new(this);
        turnStates.Add(rollState);
        moveState = new(this);
        turnStates.Add(moveState);
        /*selectPieceState = new(this);
        turnStates.Add(selectPieceState);
        selectNodeState = new(this);
        turnStates.Add(selectNodeState);*/

        GameController.Instance.OnSkipButtonUsed += _OnSkipTurn;

        SwitchToNextTurnState();
    }

    public override void ProcessTurn(float delta)
    {
        _currTurnState.ProcessTurnState(delta);
    }

    public override void EndTurn()
    {
        GameController.Instance.OnSkipButtonUsed -= _OnSkipTurn;
        base.EndTurn();
    }

    public override void AddTurnToStateQueue()
    {
        turnStates.Add(rollState);
        turnStates.Add(moveState);
    }

    private void _OnSkipTurn(){
        EmitSignal(SignalName.TurnSkipped);
        GameController.Instance.SwitchTurn();
    }
}