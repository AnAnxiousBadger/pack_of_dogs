using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CritterPathController : Path3D
{
	[Export] private Node3D _spawnPoint;
	[Export] private Node3D _reverseSpawnPoint;
	private float _spawnSeparationRadius = 1f;
	private int _crittersOnPath = 0;
	private int _crittersArrived = 0;
	public int CrittersArrived
	{
		get { return _crittersArrived; }
		set
		{
			_crittersArrived = value;
			if (_crittersArrived == _crittersOnPath)
			{
				_crittersArrived = 0;
				_crittersOnPath = 0;
				EmitSignal(SignalName.AllCrittersArrived);
			}
		}
	}
	// Listened to by CritterPathManager
	[Signal] public delegate void AllCrittersArrivedEventHandler();

	public void SpawnCritters(IPoolManager critterPool, int num, bool inversePos)
	{
		if (num > 0)
		{
			Vector3 originPos = _spawnPoint.Position;
			if (inversePos)
			{
				originPos = _reverseSpawnPoint.Position;
			}
			List<Vector2> spawnPoses = new() { new Vector2(originPos.X, originPos.Z) };
			for (int i = 1; i < num; i++)
			{
				spawnPoses.Add(GetRandomPointOnBoundary(spawnPoses));
			}

			List<CritterController> critterFlock = new();
			foreach (Vector2 point in spawnPoses)
			{
				CritterController critter = SpawnCritter(critterPool, new Vector3(point.X, 0, point.Y));
				critterFlock.Add(critter);
			}
			foreach (CritterController critter in critterFlock)
			{
				critter.flock = critterFlock;
				critter.SetCritterOnPath(this, inversePos);
			}
			_crittersOnPath = num;
		}

	}

	private Vector2 GetRandomPointAtDistance(Vector2 origin)
	{
		float angle = RandomGenerator.Instance.GetRandFloatInRange(0f, Mathf.DegToRad(360));
		return origin + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnSeparationRadius;
	}

	private Vector2 GetRandomPointOnBoundary(List<Vector2> points)
	{
		Vector2 chosenPoint;
		while (true)
		{
			Vector2 originPoint = points.LastOrDefault();
			chosenPoint = GetRandomPointAtDistance(originPoint);

			bool isOnAllPointsBoundary = true;
			foreach (Vector2 point in points)
			{
				if (point.DistanceTo(chosenPoint) < _spawnSeparationRadius)
				{
					isOnAllPointsBoundary = false;
					break;
				}
			}
			if (isOnAllPointsBoundary)
			{
				break;
			}
		}
		return chosenPoint;
	}

	private CritterController SpawnCritter(IPoolManager critterPool, Vector3 pos)
	{
		if (critterPool.GetPoolable("critter", pos) is CritterController critter)
		{
			critter.Position = pos;
			return critter;
		}
		else
		{
			return null;
		}
	}


}
