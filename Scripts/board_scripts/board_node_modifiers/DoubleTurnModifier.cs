using Godot;
using System;
using System.Net.NetworkInformation;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    [Export] private PackedScene _visualEffect;
    public override void ApplyOnStepModifier(PieceController piece)
    {
        if(piece.player == GameController.Instance.currPlayer){
            piece.player.AddTurnToStateQueue();
            GameController.Instance.visualEffectPool.PlayVisualEffect("double_turn_visual_effect", GlobalPosition);
            GameController.Instance.visualEffectPool.PlayVisualEffect("roll_button_rosette_highlight_visual_effect", GameController.Instance.boardController.boardElementsController.rollButton.GlobalPosition);
        }

        EmitSignal(SignalName.OnModifierApplied, this);
    }
}