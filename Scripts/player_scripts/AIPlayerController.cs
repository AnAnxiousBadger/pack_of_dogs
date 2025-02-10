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
        GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons = false;
        // CREATE STATES
        rollState = new(this);
        turnStates.Add(rollState);
        moveState = new(this);
        turnStates.Add(moveState);
        /*selectPieceState = new(this);
        turnStates.Add(selectPieceState);
        selectNodeState = new(this);
        turnStates.Add(selectNodeState);*/

        GlobalClassesHolder.Instance.GameController.OnSkipButtonUsed += _OnSkipTurn;

        SwitchToNextTurnState();
    }

    public override void ProcessTurn(float delta)
    {
        _currTurnState.ProcessTurnState(delta);
    }

    public override void EndTurn()
    {
        GlobalClassesHolder.Instance.GameController.OnSkipButtonUsed -= _OnSkipTurn;
        base.EndTurn();
    }

    public override void AddTurnToStateQueue()
    {
        turnStates.Add(rollState);
        turnStates.Add(moveState);
    }

    private void _OnSkipTurn(){
        EmitSignal(SignalName.TurnSkipped);
        GlobalClassesHolder.Instance.GameController.SwitchTurn();
    }
}