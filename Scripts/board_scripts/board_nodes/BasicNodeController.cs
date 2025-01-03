using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
    Node3D hihglight;
    public override void DoOnLeaveNodeAction(PieceController piece){
        EmitSignal(SignalName.Onleave, piece);
    }
    public override void DoOnStepNodeAction(PieceController piece)
    {
        piece.GlobalPosition = new Vector3(GlobalPosition.X, GlobalPosition.Y + 0.5f, GlobalPosition.Z);
        EmitSignal(SignalName.OnStep, piece);
    }

    public override void Highlight()
    {
        PackedScene highLightScene = GD.Load<PackedScene>("res://Scenes/highlight_node.tscn");
        hihglight = highLightScene.Instantiate() as Node3D;
        AddChild(hihglight);
        hihglight.Position = new Vector3(0f, 0.51f, 0f);
    }

    public override void RemoveHighlight()
    {
        hihglight?.QueueFree();
    }
}