using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RealPlayerController : BasePlayerController
{
    public PieceController selectedPiece = null;
    public List<BoardNodeController> possibeNodes = new();

    // STATES
    public RealPlayerTurnRollState rollState;
    public RealPlayerTurnSelectPieceState selectPieceState;
    public RealPlayerTurnSelectNodeState selectNodeState;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void StartTurn()
    {
        base.StartTurn();

        // CREATE STATES
        rollState = new(this);
        turnStates.Add(rollState);
        selectPieceState = new(this);
        turnStates.Add(selectPieceState);
        selectNodeState = new(this);
        turnStates.Add(selectNodeState);

        gameController.boardController.boardElementsController.skipButton.OnReleasedTickable += _OnSkipTurn;

        SwitchToNextTurnState();
    }

    public override void ProcessTurn(float delta)
    {
        _currTurnState.ProcessTurnState(delta);
    }

    public override void EndTurn()
    {
        gameController.boardController.boardElementsController.skipButton.OnReleasedTickable -= _OnSkipTurn;
    }

    public override void AddTurnToStateQueue()
    {
        turnStates.Add(rollState);
        turnStates.Add(selectPieceState);
        turnStates.Add(selectNodeState);
    }

    public void SelectPiece(PieceController piece){
        selectedPiece = piece;

        // Calculate where it can step
        possibeNodes = piece.currNode.MoveAlongNodesFromNode(roll, playerIndex, false);

        // Indicate possible destinations
        foreach (BoardNodeController node in possibeNodes)
        {
            node.Highlight();
        }
    }
    public void DeselectPiece(){
        foreach (BoardNodeController node in possibeNodes)
        {
            node.RemoveHighlight();
        }

        selectedPiece = null;
    }

    private void _OnSkipTurn(Vector3 hitPos){
        EmitSignal(SignalName.TurnSkipped);
        gameController.boardController.boardElementsController.skipButton.IsActive = false;
        gameController.SwitchTurn();
    }

    
}
