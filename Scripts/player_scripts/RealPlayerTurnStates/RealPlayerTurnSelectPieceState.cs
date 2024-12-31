using Godot;
using System;

public class RealPlayerTurnSelectPieceState : PlayerTurnBaseState
{
    private new RealPlayerController p;
    public override void EnterTurnState(){
        p.gameController._collisionMask = GameController.CollisionMask.PIECE;
    }

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta)
    {
        StaticBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse")){
            body = p.gameController.StaticBodyUnderMouse;
        }
        if(body is PieceController piece && piece.playerIndex == p.playerIndex){
            p.SelectPiece(piece);
            p.SwitchToNextTurnState();
        }
    }

    public RealPlayerTurnSelectPieceState(RealPlayerController p) : base(p){
        this.p = p;
    }
}