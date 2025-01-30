using Godot;
using System;

public partial class GoatManager : Node3D
{
	[Export] private Node3D _goatContainer;
	[Export] private GoatPathController[] _goatPaths;
	public override void _Ready()
	{
		_goatPaths[0].SpawnGoats(_goatContainer, 10, false);
	}

	
}
