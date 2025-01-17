using Godot;
using System;

public class RealPlayerTurnSelectPieceState : PlayerTurnBaseState
{
    private new RealPlayerController p;
    public override void EnterTurnState(){
        GameController.Instance.collisionMask = GameController.CollisionMask.PIECE;
    }

    public override void ExitTurnState(){
        
    }

    public override void ProcessTurnState(float delta)
    {
        StaticBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse")){
            body = GameController.Instance.StaticBodyUnderMouse;
        }
        if(body is PieceController piece && piece.playerIndex == p.playerIndex){
            p.SelectPiece(piece);
            piece.HighlightPiece();
            p.SwitchToNextTurnState();
        }
    }

    public RealPlayerTurnSelectPieceState(RealPlayerController p) : base(p){
        this.p = p;
    }
}