using Godot;
using System;

public class RealPlayerTurnSelectNodeState : PlayerTurnBaseState
{
    private new RealPlayerController p;
    public override void EnterTurnState(){}

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta)
    {
        // HANDLE DESELECT
        if((Input.IsActionJustReleased("right_mouse") || Input.IsActionJustReleased("escape")) && p.selectedPiece != null){
            p.DeselectPiece();
            p.SwitchToPreviousTurnState();
        }

        StaticBody3D body = p.CastRayFromMouse(RealPlayerController.CollisionMask.NODE);
        if(body is BoardNodeController node && p.possibeNodes.Contains(node)){

            // KICK OTHER PLAYER'S PIECES OUT
            for (int i = 0; i < node.currPieces.Count; i++)
            {
                if(node.currPieces[i].player != p){
                    StartNodeController startNode = p.gameController.boardController.GetStartNode(node.currPieces[i].player);
                    p.gameController.boardController.MovePiece(node.currPieces[i], startNode);
                }
            }

            // MOVE OWN PIECE
            p.gameController.boardController.MovePiece(p.selectedPiece, node);
            p.DeselectPiece();

            p.SwitchToNextTurnState();
            
        }

    }

    public RealPlayerTurnSelectNodeState(RealPlayerController p) : base(p){
        this.p = p;
    }
}