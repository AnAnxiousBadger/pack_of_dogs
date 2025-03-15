using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
    private VisualEffectController _hihglightEffectController;
    public override void DoOnLeaveNodeAction(PieceController piece)
    {
        EmitSignal(SignalName.OnLeave, piece);
    }
    public override void DoOnStepNodeAction(PieceController piece)
    {
        AnimationPlayer anim = GetNode("anim") as AnimationPlayer;
        anim.Play("up_and_down");
    }

    public override void Highlight()
    {
        _hihglightEffectController = GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("highlight_node_visual_effect", GlobalPosition + new Vector3(0f, 0.51f, 0f), GlobalRotation);
    }

    public override void RemoveHighlight()
    {
        _hihglightEffectController?.EndEffect();
    }
}