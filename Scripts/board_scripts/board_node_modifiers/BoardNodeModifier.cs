using Godot;
using System;

public abstract partial class BoardNodeModifier : Node3D
{
    [Export] public string modifierName;
	protected BoardNodeController _node;
    [Signal] public delegate void OnModifierAppliedEventHandler(BoardNodeModifier modifier);
    public override void _Ready()
    {
        _node = GetParent().GetParent() as BoardNodeController;
		//_node.OnStep += ApplyOnStepModifier;
		_node.OnLeave += ApplyOnLeaveModifier;
    }
    public virtual void ApplyOnStepModifier(PieceController piece){}
	protected virtual void ApplyOnLeaveModifier(PieceController piece){}
    
}
