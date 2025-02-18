using Godot;
using System;

public class RealPlayerTurnSelectPieceState : PlayerTurnBaseState
{
    private new readonly RealPlayerController p;
    public override void EnterTurnState(){
        //GlobalHelper.Instance.GameController.collisionMask = GameController.CollisionMask.PIECE;
    }

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta)
    {
        PhysicsBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse")){
            body = GlobalHelper.Instance.GameController.PhysicsBodyUnderMouse;
        }

        PieceController piece = null;
        if(body is PieceController pieceClicked && pieceClicked.playerIndex == p.playerIndex){
            piece = pieceClicked;
            /*p.SelectPiece(piece);
            piece.HighlightPiece(true);
            p.SwitchToNextTurnState();*/
        }
        else if(body is BasicNodeController nodeClicked && nodeClicked.currPieces.Count == 1 && nodeClicked.currPieces[0].playerIndex == p.playerIndex){
            piece = nodeClicked.currPieces[0];
        }
        if(piece != null){
            p.SelectPiece(piece);
            piece.HighlightPiece(true);
            p.SwitchToNextTurnState();
        }
    }

    public RealPlayerTurnSelectPieceState(RealPlayerController p) : base(p){
        this.p = p;
    }
}