using Godot;
using System;

public partial class ProtectedNodeModifier : BoardNodeModifier
{
    public override void ApplyOnStepModifier(PieceController piece)
    {
        _node.canBeSteppedOn = false;
        VisualEffectController effect = GameController.Instance.visualEffectPool.PlayVisualEffect("protected_node_modifier_visual_effect", GlobalPosition);
        effect.OnEffectEnded += _OnEffectEndedEmitModifierAppliedSignal;
        //EmitSignal(SignalName.OnModifierApplied, this);
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