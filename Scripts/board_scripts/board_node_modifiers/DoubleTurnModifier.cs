using Godot;
using System;

public partial class DoubleTurnModifier : BoardNodeModifier
{
    protected override void ApplyOnStepModifier(PieceController piece)
    {
        piece.player.AddTurnToStateQueue();
    }
}