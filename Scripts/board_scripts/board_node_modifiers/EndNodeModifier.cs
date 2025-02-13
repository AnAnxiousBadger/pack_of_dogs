using System;
using Godot;

public partial class EndNodeModifier : BoardNodeModifier
{
    
    public override void ApplyOnStepModifier(PieceController piece)
    {
        GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("piece_arrived_visual_effect", GlobalPosition + new Vector3(0f, 0.51f, 0f));
        Tween scaleDownTween = GetTree().CreateTween();
        scaleDownTween.TweenProperty(piece, "scale", new Vector3(0.3f, 0.3f, 0.3f), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        Tween positionTween = GetTree().CreateTween();
        positionTween.TweenProperty(piece, "position", GlobalHelper.Instance.GameController.boardController.GetPieceEndPos(piece), 0.35f).SetTrans(Tween.TransitionType.Cubic);


        EmitSignal(SignalName.OnModifierApplied, this);
    }
}