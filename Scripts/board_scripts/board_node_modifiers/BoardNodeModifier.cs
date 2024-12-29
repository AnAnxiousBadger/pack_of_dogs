using Godot;
using System;

public abstract partial class BoardNodeModifier : Node3D
{
	protected BoardNodeController _node;
    public override void _Ready()
    {
        _node = GetParent().GetParent() as BoardNodeController;
		_node.OnStep += ApplyOnStepModifier;
		_node.Onleave += ApplyOnLeaveModifier;
    }
    protected virtual void ApplyOnStepModifier(PieceController piece){}
	protected virtual void ApplyOnLeaveModifier(PieceController piece){}
}
