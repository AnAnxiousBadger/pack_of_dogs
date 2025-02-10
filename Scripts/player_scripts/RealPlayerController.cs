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

    /*public override void _Ready()
    {
        base._Ready();
    }*/

    public override void StartTurn()
    {
        base.StartTurn();
        GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons = true;
        // CREATE STATES
        rollState = new(this);
        turnStates.Add(rollState);
        selectPieceState = new(this);
        turnStates.Add(selectPieceState);
        selectNodeState = new(this);
        turnStates.Add(selectNodeState);

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
        turnStates.Add(selectPieceState);
        turnStates.Add(selectNodeState);
    }

    public void SelectPiece(PieceController piece){
        GlobalClassesHolder.Instance.GameController.uiController.canEscapeActOnUI = false;
        selectedPiece = piece;

        // Calculate where it can step
        possibeNodes = GlobalClassesHolder.Instance.GameController.boardController.MoveForwardAlongNodesFromNode(piece.currNode, roll, playerIndex, false);

        // Indicate possible destinations
        foreach (BoardNodeController node in possibeNodes)
        {
            node.Highlight();
        }
    }
    public void DeselectPiece(){
        GlobalClassesHolder.Instance.GameController.uiController.canEscapeActOnUI = true;
        if(selectedPiece != null){
            foreach (BoardNodeController node in possibeNodes)
            {
                node.RemoveHighlight();
            }

            selectedPiece = null;
        }
    }

    private void _OnSkipTurn(){
        EmitSignal(SignalName.TurnSkipped);
        GlobalClassesHolder.Instance.GameController.SwitchTurn();
    }

    
}
