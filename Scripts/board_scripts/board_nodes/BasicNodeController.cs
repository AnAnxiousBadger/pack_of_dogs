using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
    private VisualEffectController hihglight;
    public override void DoOnLeaveNodeAction(PieceController piece){
        EmitSignal(SignalName.OnLeave, piece);
    }
    public override void DoOnStepNodeAction(PieceController piece){
        //GameController.Instance.visualEffectPool.PlayVisualEffect("sand_dust_ring_visual_effect", TopPos);
    }

    public override void Highlight()
    {
        hihglight = GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("highlight_node_visual_effect", GlobalPosition + new Vector3(0f, 0.51f, 0f));
    }

    public override void RemoveHighlight()
    {
        hihglight?.EndEffect();
    }
}