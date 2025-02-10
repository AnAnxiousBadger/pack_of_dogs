using Godot;
using System;
using System.Net.NetworkInformation;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    [Export] private PackedScene _visualEffect;
    public override void ApplyOnStepModifier(PieceController piece)
    {
        if(piece.player == GlobalClassesHolder.Instance.GameController.currPlayer){
            piece.player.AddTurnToStateQueue();
            GlobalClassesHolder.Instance.GameController.visualEffectPool.PlayVisualEffect("double_turn_visual_effect", GlobalPosition);
            GlobalClassesHolder.Instance.GameController.visualEffectPool.PlayVisualEffect("roll_button_rosette_highlight_visual_effect", GlobalClassesHolder.Instance.GameController.boardController.boardElementsController.rollButton.GlobalPosition);
        }

        EmitSignal(SignalName.OnModifierApplied, this);
    }
}