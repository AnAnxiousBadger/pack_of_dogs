using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class GoatPathController : Path3D
{
	/*#region ToolVars
		[Export] private bool _canRunInEditor = false;
		private bool _spawn = false;
		[Export] public bool Spawn{
			get { return _spawn; }
			set {
				if(_canRunInEditor){
					_canRunInEditor = false;
					_spawn = false;
					Godot.Collections.Array<Node> children = _goatContainer.GetChildren();
					foreach (Node child in children)
					{
						child.Free();
					}
					SpawnGoats(_goatContainer, _goatNum, _reverseSpawn);
				}
			}
		}
		[Export] private Node3D _goatContainer;
		[Export] private int _goatNum = 3;
		[Export] private bool _reverseSpawn = false;
	#endregion*/
	[Export] private Node3D _spawnPoint;
	[Export] private Node3D _reverseSpawnPoint;
	[Export] private PackedScene _goatScene;
	private float _spawnSeparationRadius = 0.45f;
	public override void _Ready()
	{
	}

	public void SpawnGoats(Node3D goatContainerNode, int num, bool inversePos){
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
				GoatController goat = SpawnGoat(new Vector3(point.X, -0.5f, point.Y), goatContainerNode);
				goatFlock.Add(goat);
			}
			foreach (GoatController goat in goatFlock)
			{
				goat.flock = goatFlock;
				goat.SetGoatOnPath(this);
			}
		}
		
	}

	private Vector2 GetRandomPointAtDistance(Vector2 origin)
    {
        float angle = RandomGenerator.Instance.GetRandFInRange(0f, Mathf.DegToRad(360));
		/*RandomNumberGenerator rand = new();
		float angle = rand.RandfRange(0f, Mathf.DegToRad(360));*/
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

	private GoatController SpawnGoat(Vector3 pos, Node3D parent){
		GoatController goat = _goatScene.Instantiate() as GoatController;
		parent.AddChild(goat);
		//goat.Owner = GetTree().EditedSceneRoot;
		goat.Position = pos;
		return goat;
	}

	
}
