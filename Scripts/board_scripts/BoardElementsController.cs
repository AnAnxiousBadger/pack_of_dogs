using Godot;
using System;

public partial class BoardElementsController : Node3D
{
	[Export] public TickableButtonController rollButton;
	[Export] public PalmDisturbController palmDisturbController;
	[Signal] public delegate void OnRollDiceWithoutClickingEventHandler(BasePlayerController playerRolling);
	[Signal] public delegate void OnSkipWithoutClickingEventHandler(BasePlayerController playerRolling);
	public void HandleTickableInterActions(PhysicsBody3D currUnderMouse, PhysicsBody3D newUnderMouse, Vector3 pos){
		if(currUnderMouse != newUnderMouse){
			if(currUnderMouse is ITickable t1){
				if(Input.IsActionPressed("left_mouse")){
					t1.OnPressStopped();
				}
			}
			if(newUnderMouse is ITickable t2){
				t2.PlayHoveredEffects(pos);
				t2.OnHovered(pos);
			}
		}
		if(newUnderMouse is ITickable t3){
			if(Input.IsActionJustPressed("left_mouse")){
				t3.PlayPressedEffects(pos);
				t3.OnPressed(pos);				
			}
			if(Input.IsActionJustReleased("left_mouse")){
				t3.PlayReleasedEffects(pos);
				t3.OnReleased(pos);
			}
		}
	}

}
