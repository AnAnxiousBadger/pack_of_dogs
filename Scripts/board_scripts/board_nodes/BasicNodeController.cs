using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
    private VisualEffectController hihglight;
    public override void DoOnLeaveNodeAction(PieceController piece){
        EmitSignal(SignalName.OnLeave, piece);
    }
    public override void DoOnStepNodeAction(PieceController piece)
    {
        //EmitSignal(SignalName.OnStep, piece);
    }

    public override void Highlight()
    {
        hihglight = GameController.Instance.visualEffectPool.PlayVisualEffect("highlight_node_visual_effect", GlobalPosition + new Vector3(0f, 0.51f, 0f));
    }

    public override void RemoveHighlight()
    {
        hihglight?.EndEffect();
    }
}