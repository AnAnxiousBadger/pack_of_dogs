using Godot;
using System;

public partial class ProtectedNodeModifier : BoardNodeModifier
{
    protected override void ApplyOnStepModifier(PieceController piece)
    {
        _node.canBeSteppedOn = false;
    }
    protected override void ApplyOnLeaveModifier(PieceController piece)
    {
        _node.canBeSteppedOn = true;
    }
}