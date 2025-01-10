using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
    private VisualEffectController hihglight;
    public override void DoOnLeaveNodeAction(PieceController piece){
        EmitSignal(SignalName.Onleave, piece);
    }
    public override void DoOnStepNodeAction(PieceController piece)
    {
        EmitSignal(SignalName.OnStep, piece);
    }

    public override void Highlight()
    {
        hihglight = _gameController.visualEffectPool.PlayVisualEffect("highlight_node_visual_effect", GlobalPosition + new Vector3(0f, 0.51f, 0f));
    }

    public override void RemoveHighlight()
    {
        hihglight?.EndEffect();
    }
}