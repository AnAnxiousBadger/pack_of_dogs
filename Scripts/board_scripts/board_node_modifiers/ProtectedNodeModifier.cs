using Godot;
using System;

public partial class ProtectedNodeModifier : BoardNodeModifier
{
    public override void ApplyOnStepModifier(PieceController piece)
    {
        _node.canBeSteppedOn = false;
        VisualEffectController effect = GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("protected_node_modifier_visual_effect", GlobalPosition);
        effect.OnEffectEnded += _OnEffectEndedEmitModifierAppliedSignal;
    }
    protected override void ApplyOnLeaveModifier(PieceController piece)
    {
        _node.canBeSteppedOn = true;
    }
    protected void _OnEffectEndedEmitModifierAppliedSignal(VisualEffectController effect){
        effect.OnEffectEnded -= _OnEffectEndedEmitModifierAppliedSignal;
        EmitSignal(SignalName.OnModifierApplied, this);
    }
}