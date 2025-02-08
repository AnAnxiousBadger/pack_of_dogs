using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GoatPathController : Path3D
{
	[Export] private Node3D _spawnPoint;
	[Export] private Node3D _reverseSpawnPoint;
	private float _spawnSeparationRadius = 1f;
	private int _goatsOnPath = 0;
	private int _goatsArrived = 0;
	public int GoatsArrived {
		get { return _goatsArrived;}
		set {
			_goatsArrived = value;
			if(_goatsArrived == _goatsOnPath){
				_goatsArrived = 0;
				_goatsOnPath = 0;
				EmitSignal(SignalName.AllGoatsArrived);
			}
		}
	}
	[Signal] public delegate void AllGoatsArrivedEventHandler();

	public void SpawnGoats(/*PackedScene goatScene, Node3D goatContainerNode,*/IPoolManager goatPool, int num, bool inversePos){
		if(num > 0){
			Vector3 originPos = _spawnPoint.Position;
			if(inversePos){
				originPos = _reverseSpawnPoint.Position;
			}
			List<Vector2> spawnPoses = new () { new Vector2(originPos.X, originPos.Z) };
			for (int i = 1; i < num; i++)
			{
				spawnPoses.Add(GetRandomPointOnBoundary(spawnPoses));
			}

			List<GoatController> goatFlock = new();
			foreach (Vector2 point in spawnPoses)
			{
				GoatController goat = SpawnGoat(goatPool,/*goatScene,*/ new Vector3(point.X, 0, point.Y)/*, goatContainerNode*/);
				goatFlock.Add(goat);
			}
			foreach (GoatController goat in goatFlock)
			{
				goat.flock = goatFlock;
				goat.SetGoatOnPath(this, inversePos);
			}
			_goatsOnPath = num;
		}
		
	}

	private Vector2 GetRandomPointAtDistance(Vector2 origin)
    {
        float angle = RandomGenerator.Instance.GetRandFInRange(0f, Mathf.DegToRad(360));
		return origin + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnSeparationRadius;
    }

	private Vector2 GetRandomPointOnBoundary(List<Vector2> points){
		Vector2 chosenPoint;
		while(true){
			Vector2 originPoint = points.LastOrDefault();
			chosenPoint = GetRandomPointAtDistance(originPoint);

			bool isOnAllPointsBoundary = true;
			foreach (Vector2 point in points)
			{
				if(point.DistanceTo(chosenPoint) < _spawnSeparationRadius){
					isOnAllPointsBoundary = false;
					break;
				}
			}
			if(isOnAllPointsBoundary){
				break;
			}
		}
		return chosenPoint;
	}

	private GoatController SpawnGoat(IPoolManager goatPool,/*PackedScene goatScene,*/ Vector3 pos/* Node3D parent*/){
		//GoatController goat = goatScene.Instantiate() as GoatController;
		//parent.AddChild(goat);
		if(goatPool.GetPoolable("goat", pos) is GoatController goat){
			goat.Position = pos;
			return goat;
		}
		else{
			return null;
		}
		/*goat.Position = pos;
		return goat;*/
	}

	
}
