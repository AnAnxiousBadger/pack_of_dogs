using Godot;
using System;

public partial class GeneralTickable : StaticBody3D, ITickable
{
    [Export] public bool IsActive { get; set; } = true;
    public Node3D TickableNode {get  => this;}
    [Export] public TickableVisualEffect[] Effects { get; set; } = Array.Empty<TickableVisualEffect>();
    [Export] public TickableSoundEffect[] SoundEffects {get; set;} = Array.Empty<TickableSoundEffect>();

    public Vector3 GetGlobalPos()
    {
        return GlobalPosition;
    }

    public void OnHovered(Vector3 pos)
    {
        return;
    }

    public void OnPressed(Vector3 pos)
    {
        return;
    }

    public void OnReleased(Vector3 pos)
    {
        return;
    }
}
