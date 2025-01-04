using Godot;
using System;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    [Export] private PackedScene _visualEffect;
    protected override void ApplyOnStepModifier(PieceController piece)
    {
        piece.player.AddTurnToStateQueue();
        
        BoardNodeVisualEffectController visualEffect =  (BoardNodeVisualEffectController)_visualEffect.Instantiate();
        AddChild(visualEffect);
        visualEffect.PlayVisualEffect();
    }
}