using Godot;
using System;

public class RealPlayerTurnSelectPieceState : PlayerTurnBaseState
{
    private new readonly RealPlayerController p;
    public override void EnterTurnState(){
        GlobalClassesHolder.Instance.GameController.collisionMask = GameController.CollisionMask.PIECE;
    }

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta)
    {
        PhysicsBody3D body = null;
        if(Input.IsActionJustReleased("left_mouse")){
            body = GlobalClassesHolder.Instance.GameController.PhysicsBodyUnderMouse;
        }
        if(body is PieceController piece && piece.playerIndex == p.playerIndex){
            p.SelectPiece(piece);
            piece.HighlightPiece(true);
            p.SwitchToNextTurnState();
        }
    }

    public RealPlayerTurnSelectPieceState(RealPlayerController p) : base(p){
        this.p = p;
    }
}