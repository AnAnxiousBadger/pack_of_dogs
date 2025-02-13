using Godot;
using System;

public partial class MainMenuZigguratController : Node3D
{
	public bool scaleZiggurats;
	[Export] private MeshInstance3D[] _zigguratMeshes;
	[Export] private float _zigguratMovingSpeed = 0.1f;
	[Export] private float _zigguratMaxScale = 1.5f;

	
	public override void _Process(double delta)
	{
		if(scaleZiggurats){
			float coeff = 1 + _zigguratMovingSpeed * (float)delta;
			foreach (MeshInstance3D ziggurat in _zigguratMeshes)
			{
				float scale = ziggurat.Scale.X * coeff;
				if(scale > 1.5f){
					scale = 0.0625f;
				}
				ziggurat.Scale = new Vector3(scale, scale, scale);
				
			}
		}
	}

	private void Reset(){
		for (int i = 0; i < _zigguratMeshes.Length; i++)
		{
			float scale = 1f /Mathf.Pow(2f, i);
			_zigguratMeshes[i].Scale = new Vector3(scale, scale, scale);
		}
	} 
}
