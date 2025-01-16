using Godot;
using System;

public partial class BoardElementsController : Node3D
{
	[Export] public TickableController rollButton;
	public void HandleTickableInterActions(StaticBody3D currUnderMouse, StaticBody3D newUnderMouse, Vector3 pos){
		if(currUnderMouse != newUnderMouse){
			if(currUnderMouse is TickableController t1){
				if(Input.IsActionPressed("left_mouse")){
					t1.PressStopped();
				}
			}
			if(newUnderMouse is TickableController t2){
				t2.Hover(pos);
			}
		}
		if(newUnderMouse is TickableController t3){
			if(Input.IsActionJustPressed("left_mouse")){
				t3.Press(pos);
			}
			if(Input.IsActionJustReleased("left_mouse")){
				t3.Release(pos);
			}
		}
	}

}
