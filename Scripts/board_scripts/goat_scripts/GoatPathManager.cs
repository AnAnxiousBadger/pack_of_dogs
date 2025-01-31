using Godot;
using System;

public partial class GoatPathManager : Node3D
{
	[Export] private Node3D _goatContainer;
	[Export] private GoatPathController[] _goatPaths;
	[Export] private Vector2I _minMaxGoatNum = new Vector2I (3, 6);
	[Export] private PackedScene _goatScene;
	[Export] private Timer _goatSpawnTimer;
	public override void _Ready()
	{
		_goatSpawnTimer.Timeout += SpawnOnRandomGoatPath;
		foreach (GoatPathController path in _goatPaths)
		{
			path.AllGoatsArrived += RestartGoatTimer;
		}
		_goatSpawnTimer.Start();
	}
	private void RestartGoatTimer(){
		_goatSpawnTimer.Start();
	}
	private void SpawnOnRandomGoatPath(){
		int path = RandomGenerator.Instance.GetRandIntInRange(0, _goatPaths.Length - 1);
		int num = RandomGenerator.Instance.GetRandIntInRange(_minMaxGoatNum.X, _minMaxGoatNum.Y);
		bool dir = RandomGenerator.Instance.GetRandomBool();
		path = 2;
		_goatPaths[path].SpawnGoats(_goatScene, _goatContainer, num, dir);
	}

	
}
