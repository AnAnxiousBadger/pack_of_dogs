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
        GameController.Instance.allowClicksOnTickableButtons = true;
        // CREATE STATES
        rollState = new(this);
        turnStates.Add(rollState);
        selectPieceState = new(this);
        turnStates.Add(selectPieceState);
        selectNodeState = new(this);
        turnStates.Add(selectNodeState);

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
        turnStates.Add(selectPieceState);
        turnStates.Add(selectNodeState);
    }

    public void SelectPiece(PieceController piece){
        GameController.Instance.uiController.canEscapeActOnUI = false;
        selectedPiece = piece;

        // Calculate where it can step
        possibeNodes = GameController.Instance.boardController.MoveForwardAlongNodesFromNode(piece.currNode, roll, playerIndex, false);

        // Indicate possible destinations
        foreach (BoardNodeController node in possibeNodes)
        {
            node.Highlight();
        }
    }
    public void DeselectPiece(){
        GameController.Instance.uiController.canEscapeActOnUI = true;
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
        GameController.Instance.SwitchTurn();
    }

    
}
