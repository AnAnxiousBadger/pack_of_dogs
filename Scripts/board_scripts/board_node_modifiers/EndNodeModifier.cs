using System;
using Godot;

public partial class EndNodeModifier : BoardNodeModifier
{
    //[Export] private PackedScene _rigidPieceScene;
    //[Export] private float _rigidPieceSpawnRadius = 0.25f;
    
    public override void ApplyOnStepModifier(PieceController piece)
    {
        piece.hasArrived = true;
        _node.currPieces.Remove(piece);
        //piece.Visible = false;
        CollisionShape3D shape = (CollisionShape3D) piece.GetChild(1);
        shape.Disabled = true;

        /*RigidPieceController rigidPiece = _rigidPieceScene.Instantiate() as RigidPieceController;
        AddChild(rigidPiece);
        rigidPiece.GlobalPosition = _node.TopPos;
        rigidPiece.GravityScale = 0f;
        rigidPiece.ApplyMotion();*/
        //GameController.Instance.visualEffectPool.PlayVisualEffect("gold_explosion_visual_effect", piece.GlobalPosition);
        Tween scaleDownTween = GetTree().CreateTween();
        scaleDownTween.TweenProperty(piece, "scale", new Vector3(0.3f, 0.3f, 0.3f), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        Tween positionTween = GetTree().CreateTween();
        /*Vector3 targetPos = GetRandomPointOnSquare(0.7f);
        GD.Print(targetPos + " + " + GlobalPosition);
        targetPos += GlobalPosition;
        GD.Print(targetPos);
        targetPos.Y = 1f;
        GD.Print(piece.ToLocal(targetPos));*/
        positionTween.TweenProperty(piece, "position", GameController.Instance.boardController.GetPieceEndPos(piece), 0.35f).SetTrans(Tween.TransitionType.Cubic);


        EmitSignal(SignalName.OnModifierApplied, this);
    }

    private Vector3 GetRandomPositionInRadius(float outerRadius, float innerRadius){
		float r = RandomGenerator.Instance.GetRandFInRange(innerRadius, outerRadius);
		float angle = RandomGenerator.Instance.GetRandFInRange(0f, 360f);
		return new Vector3(r * Mathf.Cos(Mathf.DegToRad(angle)), 0f, r * Mathf.Sin(Mathf.DegToRad(angle)));
	}
    private Vector3 GetRandomPointOnSquare(float a){
        return new Vector3 (RandomGenerator.Instance.GetRandFInRange(- a / 2, a / 2), 0f, RandomGenerator.Instance.GetRandFInRange(- a / 2, a / 2));
    }
}