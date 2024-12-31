using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RealPlayerController : BasePlayerController
{
    private bool didSelectPiece = false;
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

        gameController.uiController.SkipTurnButtonUp += _OnSkipTurn;

        SwitchToNextTurnState();
    }

    public override void ProcessTurn(float delta)
    {
        _currTurnState.ProcessTurnState(delta);
    }

    public override void EndTurn()
    {
        gameController.uiController.SkipTurnButtonUp -= _OnSkipTurn;
    }

    public override void AddTurnToStateQueue()
    {
        turnStates.Add(rollState);
        turnStates.Add(selectPieceState);
        turnStates.Add(selectNodeState);
    }

    public void SelectPiece(PieceController piece){
        selectedPiece = piece;
        didSelectPiece = true;

        // Indicate that the piece is selected
        MeshInstance3D m = (MeshInstance3D)piece.GetChild(0);
        Material mat = GD.Load<Material>("res://Assets/Materials/red_mat.tres");
        m.SetSurfaceOverrideMaterial(0, mat);

        // Calculate where it can step
        possibeNodes = piece.currNode.MoveAlongNodesFromNode(roll, playerIndex, false);

        // Indicate possible destinations
        foreach (BoardNodeController node in possibeNodes)
        {
            node.Highlight();
        }
    }
    public void DeselectPiece(){
        MeshInstance3D m = (MeshInstance3D)selectedPiece.GetChild(0);
        Material mat;
        if(selectedPiece.playerIndex == 0){
            mat = GD.Load<Material>("res://Assets/Materials/white_mat.tres");
        }
        else{
            mat = GD.Load<Material>("res://Assets/Materials/black_mat.tres");
        }
        m.SetSurfaceOverrideMaterial(0, mat);

        foreach (BoardNodeController node in possibeNodes)
        {
            node.RemoveHighlight();
        }

        selectedPiece = null;
        didSelectPiece = false;
    }

    private void _OnSkipTurn(){
        EmitSignal(SignalName.TurnSkipped);
        gameController.uiController.SetSkipButtonActivity(false);
        gameController.SwitchTurn();
    }

    
}
