using Godot;
using System;
using System.Net.NetworkInformation;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    public override void ApplyOnStepModifier(PieceController piece)
    {
        if(piece.player == GlobalHelper.Instance.GameController.currPlayer){
            piece.player.AddTurnToStateQueue();
            GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("double_turn_visual_effect", GlobalPosition, GlobalRotation);
            GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("roll_button_rosette_highlight_visual_effect", GlobalHelper.Instance.GameController.boardController.boardElementsController.rollButton.GlobalPosition, GlobalHelper.Instance.GameController.boardController.boardElementsController.rollButton.GlobalRotation);
        }

        EmitSignal(SignalName.OnModifierApplied, this);
    }
}