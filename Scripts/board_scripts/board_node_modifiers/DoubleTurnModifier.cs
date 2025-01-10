using Godot;
using System;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    [Export] private PackedScene _visualEffect;
    protected override void ApplyOnStepModifier(PieceController piece)
    {
        piece.player.AddTurnToStateQueue();
        
        piece.player.gameController.visualEffectPool.PlayVisualEffect("double_turn_visual_effect", GlobalPosition);
    }
}