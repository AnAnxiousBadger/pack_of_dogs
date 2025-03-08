using Godot;
using System;

public partial class PalmTickable : StaticBody3D, ITickable
{
    [Export] public bool IsActive { get; set; } = true;
    [Export] public TickableVisualEffect[] Effects {get; set; } = Array.Empty<TickableVisualEffect>();
    [Export] public AudioLibrary AudioLibrary { get; set; }
    public Node3D TickableNode {get  => this;}

    public Vector3 GetGlobalPos()
    {
        return GlobalPosition;
    }
    public Vector3 GetGlobalRot(){
        return GlobalRotation;
    }

    public void OnHovered(Vector3 pos)
    {
        return;
    }

    public void OnPressed(Vector3 pos)
    {
        GlobalHelper.Instance.GameController.boardController.boardElementsController.palmDisturbController.Distrub(pos);
    }

    public void OnReleased(Vector3 pos)
    {
        return;
    }
}