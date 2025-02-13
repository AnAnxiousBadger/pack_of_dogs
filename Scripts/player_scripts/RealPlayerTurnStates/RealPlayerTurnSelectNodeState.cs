using Godot;
using System;
using System.Collections.Generic;

public class RealPlayerTurnSelectNodeState : PlayerTurnBaseState
{
    private new readonly RealPlayerController p;
    public override void EnterTurnState(){
        GlobalHelper.Instance.GameController.collisionMask = GameController.CollisionMask.NODE;
    }

    public override void ExitTurnState(){
        p.selectedPiece?.RemoveHighlight();
        p.DeselectPiece();
    }

    public override void ProcessTurnState(float delta)
    {
        // HANDLE DESELECT
        if((Input.IsActionJustReleased("right_mouse") || Input.IsActionJustReleased("escape")) && p.selectedPiece != null){
            p.selectedPiece.RemoveHighlight();
            p.DeselectPiece();
            p.SwitchToPreviousTurnState();
        }
        PhysicsBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse") && p.selectedPiece != null){
            body = GlobalHelper.Instance.GameController.PhysicsBodyUnderMouse;
        }

        if(body is BoardNodeController node && p.possibeNodes.Contains(node)){
            // MOVE OWN PIECE
            node.OnAllOnStepModifierApplied += _OnPieceMovedAndProcessed;
            GlobalHelper.Instance.GameController.boardController.MovePiece(p.selectedPiece, node, false);
            p.EmitSignal(BasePlayerController.SignalName.PieceMoved, p.roll);
            p.DeselectPiece();
        }
    }

    private void _OnPieceMovedAndProcessed(BoardNodeController destination){
        destination.OnAllOnStepModifierApplied -= _OnPieceMovedAndProcessed;
        p.SwitchToNextTurnState();
    }

    public RealPlayerTurnSelectNodeState(RealPlayerController p) : base(p){
        this.p = p;
    }
}