using Godot;
using System;
using System.Collections.Generic;

public class RealPlayerTurnSelectNodeState : PlayerTurnBaseState
{
    private new RealPlayerController p;
    public override void EnterTurnState(){
        p.gameController._collisionMask = GameController.CollisionMask.NODE;
    }

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta)
    {
        // HANDLE DESELECT
        if((Input.IsActionJustReleased("right_mouse") || Input.IsActionJustReleased("escape")) && p.selectedPiece != null){
            p.selectedPiece.RemoveHighlight();
            p.DeselectPiece();
            p.SwitchToPreviousTurnState();
        }
        StaticBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse") && p.selectedPiece != null){
            body = p.gameController.StaticBodyUnderMouse;
        }

        if(body is BoardNodeController node && p.possibeNodes.Contains(node)){
            // KICK OTHER PLAYER'S PIECES OUT
            /*List<PieceController> enemyPiecesOnNode = node.GetEnemyPieces(p);
            if(enemyPiecesOnNode.Count > 0){
                for (int i = 0; i < enemyPiecesOnNode.Count; i++)
                {
                    StartNodeController startNode = enemyPiecesOnNode[i].player.gameController.boardController.GetStartNode(enemyPiecesOnNode[i].player);
                    // EMIT SIGNALS
                    p.EmitSignal(BasePlayerController.SignalName.EnemyPieceHit);
                    enemyPiecesOnNode[i].player.EmitSignal(BasePlayerController.SignalName.PieceHit);
                    // MOVE PIECE
                    enemyPiecesOnNode[i].player.gameController.boardController.MovePiece(node.currPieces[i], startNode, true);
                }
            }*/

            // MOVE OWN PIECE
            node.OnAllOnStepModifierApplied += _OnPieceMovedAndProcessed;
            GameController.Instance.boardController.MovePiece(p.selectedPiece, node, false);
            p.EmitSignal(BasePlayerController.SignalName.PieceMoved, p.roll);
            
            p.DeselectPiece();

            //p.SwitchToNextTurnState();
            
        }
    }

    private void _OnPieceMovedAndProcessed(BoardNodeController destination){
        destination.OnAllOnStepModifierApplied -= _OnPieceMovedAndProcessed;
        //p.DeselectPiece();
        p.SwitchToNextTurnState();
    }
    /*private void _OnPieceArricedToNode(PieceController piece, BoardNodeController node){
        piece.OnPiecesArrivedToNode -= _OnPieceArricedToNode;
        p.SwitchToNextTurnState();
    }*/

    public RealPlayerTurnSelectNodeState(RealPlayerController p) : base(p){
        this.p = p;
    }
}