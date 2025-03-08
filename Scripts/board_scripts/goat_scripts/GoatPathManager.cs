using Godot;
using System;
using System.Collections.Generic;

public partial class GoatPathManager : Node3D, IPoolManager
{
	// EXPORTS
	[Export] public AudioLibrary goatAudioLibrary;
	[Export] private Node3D _goatContainer;
	[Export] private GoatPathController[] _goatPaths;
	[Export] private Vector2I _minMaxGoatNum = new (3, 6);
	[Export] private Timer _goatSpawnTimer;
    [Export] public PoolableSettings[] PoolableScenes { get; set; }
    public Dictionary<string, Queue<IPoolable>> PoolablesDict { get; set; }

    public override void _Ready()
	{
		((IPoolManager)this).SpawnPoolables(_goatContainer);
		_goatSpawnTimer.Timeout += SpawnOnRandomGoatPath;
		foreach (GoatPathController path in _goatPaths)
		{
			path.AllGoatsArrived += RestartGoatTimer;
		}
		_goatSpawnTimer.Start();
	}
	public IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos)
    {
        if(poolable is GoatController goat){
			goat.IsActive = true;
		}
		return poolable;
    }
	public IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos, Vector3 rot)
    {
        return DoOnPoolingAction(poolable, pos);
    }

    public void DoOnReQueueToPoolAction(IPoolable poolable){
		if(poolable is GoatController goat){
			goat.IsActive = false;
		}
	}
	private void RestartGoatTimer(){
		_goatSpawnTimer.Start();
	}
	private void SpawnOnRandomGoatPath(){
		int path = RandomGenerator.Instance.GetRandIntInRange(0, _goatPaths.Length - 1);
		int num = RandomGenerator.Instance.GetRandIntInRange(_minMaxGoatNum.X, _minMaxGoatNum.Y);
		bool dir = RandomGenerator.Instance.GetRandBool();
		_goatPaths[path].SpawnGoats(this, num, dir);
	}

	
}
