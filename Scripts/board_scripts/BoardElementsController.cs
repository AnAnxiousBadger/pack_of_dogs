using Godot;
using System;

public partial class BoardElementsController : Node3D
{
	// EXPORTS
	[Export] public TickableController rollButton;
	[Export] public TickableController skipButton;

	public void HandleClickTickable(TickableController tickable, Vector3 hitPos){
		if(Input.IsActionJustPressed("left_mouse")){
			tickable.OnPressed(hitPos);
		}
		if(Input.IsActionJustReleased("left_mouse")){
			tickable.OnReleased(hitPos);
		}
	}
}
