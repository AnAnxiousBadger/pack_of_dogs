using Godot;
using System;
[Tool]
public partial class PalmTool : Node3D
{
	[Export] private bool _canRunInEditor;
	[Export] public bool SetChildren {
		get { return false; }
		set {
			if(_canRunInEditor){
				SetUpChildren();
				_canRunInEditor = false;
			}
		}
	}
	[Export] public bool ClearChildren {
		get { return false; }
		set {
			if(_canRunInEditor){
				ClearUpChildren();
				_canRunInEditor = false;
			}
		}
	}
 
    [Export] private PackedScene _palmTreeUtil;

	private void SetUpChildren(){
		Godot.Collections.Array<Node> children = GetChildren();

		for (int i = 0; i < children.Count; i++)
		{
			if(children[i] is MeshInstance3D child){

				StaticBody3D body = _palmTreeUtil.Instantiate() as StaticBody3D;
				AddChild(body);
				body.Owner = GetTree().EditedSceneRoot;

				body.Name = child.Name;
				body.Position = child.Position;

				body.Scale = child.Scale;
				body.Rotation = child.Rotation;


				child.Visible = false;
			}
		}
	}
	private void ClearUpChildren()
    {
        Godot.Collections.Array<Node> children = GetChildren();

		for (int i = 0; i < children.Count; i++)
		{
			if(children[i] is StaticBody3D body && children[i] is not ITickable){
				
				body.Free();				
			}
			else if(children[i] is MeshInstance3D mesh)
			{
				mesh.Visible = true;
			}
		}
    }
}
