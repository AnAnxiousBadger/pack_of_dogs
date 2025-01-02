using System;
using Godot;

public partial class BasicNodeController : BoardNodeController
{
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
        Material mat = GD.Load<Material>("res://Assets/materials/base_color_materials/orange_mat.tres");
        _mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public override void RemoveHighlight()
    {
        Material mat = GD.Load<Material>("res://Assets/materials/base_color_materials/yellow_mat.tres");
        _mesh.SetSurfaceOverrideMaterial(0, mat);
    }
}