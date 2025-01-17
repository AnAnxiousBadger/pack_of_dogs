using System;
using Godot;

public partial class EndNodeModifier : BoardNodeModifier
{
    
    public override void ApplyOnStepModifier(PieceController piece)
    {
        Tween scaleDownTween = GetTree().CreateTween();
        scaleDownTween.TweenProperty(piece, "scale", new Vector3(0.3f, 0.3f, 0.3f), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        Tween positionTween = GetTree().CreateTween();
        positionTween.TweenProperty(piece, "position", GameController.Instance.boardController.GetPieceEndPos(piece), 0.35f).SetTrans(Tween.TransitionType.Cubic);


        EmitSignal(SignalName.OnModifierApplied, this);
    }
}