using Godot;
using System;
using System.Collections.Generic;

public class RealPlayerTurnSelectNodeState : PlayerTurnBaseState
{
    private new readonly RealPlayerController p;
    public override void EnterTurnState() { }

    public override void ExitTurnState()
    {
        p.selectedPiece?.RemoveHighlight();
        p.DeselectPiece();
    }

    public override void ProcessTurnState(float delta)
    {
        // HANDLE DESELECT
        if ((Input.IsActionJustReleased("right_mouse") || Input.IsActionJustReleased("escape")) && p.selectedPiece != null)
        {
            p.selectedPiece.RemoveHighlight();
            p.DeselectPiece();
            p.SwitchToPreviousTurnState();
        }
        PhysicsBody3D body = null;
        if (Input.IsActionJustReleased("left_mouse") && p.selectedPiece != null)
        {
            body = GlobalHelper.Instance.GameController.PhysicsBodyUnderMouse;
        }

        BoardNodeController node = null;
        if (body is BoardNodeController ownNodeClicked && ownNodeClicked == p.selectedPiece.currNode)
        {
            // DESELECT IF CLICKED ON CURRENT NODE
            p.selectedPiece.RemoveHighlight();
            p.DeselectPiece();
            p.SwitchToPreviousTurnState();
        }
        else
        {
            if (body is BoardNodeController nodeClicked && p.possibeNodes.Contains(nodeClicked))
            {
                node = nodeClicked;
            }
            else if (body is PieceController pieceClicked && p.possibeNodes.Contains(pieceClicked.currNode))
            {
                node = pieceClicked.currNode;
            }
            if (node != null)
            {
                // MOVE OWN PIECE
                node.OnAllOnStepModifierApplied += _OnPieceMovedAndProcessed;
                GlobalHelper.Instance.GameController.boardController.MovePiece(p.selectedPiece, node, false);
                p.EmitSignal(BasePlayerController.SignalName.PieceMoved, p.roll);
                p.DeselectPiece();
            }
        }
    }

    private void _OnPieceMovedAndProcessed(BoardNodeController destination)
    {
        destination.OnAllOnStepModifierApplied -= _OnPieceMovedAndProcessed;
        p.SwitchToNextTurnState();
    }

    public RealPlayerTurnSelectNodeState(RealPlayerController p) : base(p)
    {
        this.p = p;
    }
}