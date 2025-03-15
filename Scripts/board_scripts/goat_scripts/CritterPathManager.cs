using Godot;
using System;
using System.Collections.Generic;

public partial class CritterPathManager : Node3D, IPoolManager
{
	// EXPORTS
	[Export] private Node3D _critterContainer;
	[Export] private CritterPathController[] _critterPaths;
	[Export] private Vector2I _minMaxCritterNum = new(3, 6);
	[Export] private Timer _critterSpawnTimer;
	[Export] public PoolableSettings[] PoolableScenes { get; set; }
	public Dictionary<string, Queue<IPoolable>> PoolablesDict { get; set; }

	public override void _Ready()
	{
		((IPoolManager)this).SpawnPoolables(_critterContainer);
		_critterSpawnTimer.Timeout += SpawnOnRandomCritterPath;
		foreach (CritterPathController path in _critterPaths)
		{
			path.AllCrittersArrived += RestartCritterTimer;
		}
		_critterSpawnTimer.Start();
	}
	public IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos)
	{
		if (poolable is CritterController critter)
		{
			critter.IsActive = true;
		}
		return poolable;
	}
	public IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos, Vector3 rot)
	{
		return DoOnPoolingAction(poolable, pos);
	}

	public void DoOnReQueueToPoolAction(IPoolable poolable)
	{
		if (poolable is CritterController critter)
		{
			critter.IsActive = false;
		}
	}
	private void RestartCritterTimer()
	{
		_critterSpawnTimer.Start();
	}
	private void SpawnOnRandomCritterPath()
	{
		int path = RandomGenerator.Instance.GetRandIntInRange(0, _critterPaths.Length - 1);
		int num = RandomGenerator.Instance.GetRandIntInRange(_minMaxCritterNum.X, _minMaxCritterNum.Y);
		bool dir = RandomGenerator.Instance.GetRandBool();
		_critterPaths[path].SpawnCritters(this, num, dir);
	}


}
