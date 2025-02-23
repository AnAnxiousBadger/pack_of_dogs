using Godot;
using System;
using System.Collections.Generic;

public partial class GoatController : CharacterBody3D, ITickable, IPoolable
{
	public bool IsActive {get; set;}
	public Node3D TickableNode {get  => this;}
	// EXPORTS
	[ExportCategory("Common Settings")]
	[Export] public TickableVisualEffect[] Effects {get; set;}
	[Export] public AudioLibrary AudioLibrary { get; set; }
	[Export] private AnimationPlayer _anim;
	[Export] private Timer _randomVelocityTimer;

	[ExportCategory("Boid Settings")]
	[Export] private float _separationRadius = 0.4f;
	[Export] private float _separationFactor = 1f;
	[Export] private float _alignmentFactor = 0f;
	[Export] private float _cohesionFactor = 0f;

	[ExportCategory("Path Follow Settings")]
	[Export] private float _minDistanceFromPathPosition = 0.5f;
	[Export] private float _pathFollowFactor = 0.1f;

	[ExportCategory("Velocity Settings")]
	[Export] private float _speed = 0.15f;
	[Export] private float _randomVelocityFactor = 0.03f;

	// REFERENCES
	public List<GoatController> flock = new();
	private GoatPathController _goatPath;
	private Queue<Vector3> _pathPoses = new();
	private Vector3 _currPathPosTarget;
	private AudioStreamPlayer3D _tempAudioStreamPlayer = null;
	
	// OTHERS
	private Vector3 _randomVelocity = Vector3.Zero;
	private bool _canBleat = true;
	
	// POLLABLE
	public IPoolManager Pool {get; set;}
    public string Identifier { get; set; }
    public Node PoolableNode => this;

    public override void _Ready()
    {
		IsActive = false;
		_separationRadius = RandomGenerator.Instance.GetRandFloatInRange(0.35f, 0.45f);
		_anim.Seek(RandomGenerator.Instance.GetRandFloatInRange(0f, 1.9f));
		_randomVelocityTimer.WaitTime = RandomGenerator.Instance.GetRandFloatInRange(3f, 5f);
		_randomVelocityTimer.Timeout += _OnTimerTimeOut;
		_randomVelocityTimer.Start();
    }
    public override void _PhysicsProcess(double delta)
	{
		if(IsActive){
			Velocity = Vector3.Zero;
			(Vector3 separationVelocity, Vector3 alignmentVelocity, Vector3 cohesionVelocity) = FlockSteer();
			Velocity += separationVelocity;
			Velocity += alignmentVelocity;
			Velocity += cohesionVelocity;

			Velocity += GetVelocityToPath();

			Velocity += _randomVelocity * _randomVelocityFactor;

			ClampVelocity();
			Velocity = new Vector3(Velocity.X, 0f, Velocity.Z);
			Position += Velocity * (float)delta;
			UpdateRotation((float)delta);
		}
	}
	public void SetGoatOnPath(GoatPathController path, bool isInverse){
		_goatPath = path;
		Curve3D pathCurve = path.Curve;
		if(!isInverse){
			for (int i = 0; i < pathCurve.PointCount; i++)
			{
				_pathPoses.Enqueue(pathCurve.GetPointPosition(i));
			}
		}
		else{
			for (int i = pathCurve.PointCount - 1; i > -1; i--)
			{
				_pathPoses.Enqueue(pathCurve.GetPointPosition(i));
			}
		}
		
		GetNextPosTarget();
	}
	private void GetNextPosTarget(){
		if(_pathPoses.Count > 0){
			_currPathPosTarget = _pathPoses.Dequeue();
		}
		else{
			for (int i = 0; i < flock.Count; i++)
			{
				if(flock[i] != this){
					for (int j = 0; j < flock[i].flock.Count; j++)
					{
						if(flock[i].flock[j] == this){
							flock[i].flock.RemoveAt(j);
							break;
						}
					}
				}
			}
			_goatPath.GoatsArrived ++;
			Pool.ReQueuePoolable(this);
		}
	}
	private Vector3 GetVelocityToPath(){
		if(Position.DistanceTo(_currPathPosTarget) < _minDistanceFromPathPosition){	
			GetNextPosTarget();
		}
		return (_currPathPosTarget - Position).Normalized() * _pathFollowFactor;
	}
	private (Vector3, Vector3, Vector3) FlockSteer(){
		Vector3 separationVelocity = Vector3.Zero;
		Vector3 alignmentVelocity = Vector3.Zero;
		Vector3 cohesionVelocity = Vector3.Zero;
		Vector3 cohesionPoint = Vector3.Zero;

		int numToAvoid = 0;
		int numToAlign = 0;
		int numToCoh = 0;
		Vector3 pos = Position;
		foreach (GoatController goat in flock)
		{
			if(goat != this){
				Vector3 otherPos = goat.Position;
				float dist = (pos - otherPos).Length();
				if(dist < _separationRadius){
					Vector3 otherGoatToThis = pos - otherPos;
					Vector3 dirToTravel = otherGoatToThis.Normalized();
					separationVelocity += dirToTravel * (1 - dist / _separationRadius);
					numToAvoid++;
				}

				alignmentVelocity += goat.Velocity;
				numToAlign++;

				cohesionPoint += otherPos;
				numToCoh++;
			}
			
		}
		if(numToAvoid > 0){
			separationVelocity /= (float)numToAvoid;
			separationVelocity.Normalized();
			separationVelocity *= _separationFactor;
		}
		if(numToAlign > 0){
			alignmentVelocity /= (float)numToAlign;
			alignmentVelocity *= _alignmentFactor;
		}
		if(numToCoh > 0){
			cohesionPoint /= (float)numToCoh;
			Vector3 cohesionDir = cohesionPoint - pos;
			cohesionDir.Normalized();
			cohesionVelocity = cohesionDir * _cohesionFactor;
		}
		
		return (separationVelocity, alignmentVelocity, cohesionVelocity);
	}

	private void ClampVelocity(){
		Vector3 dir = Velocity.Normalized();
		Velocity = dir * _speed;
	}

	private void UpdateRotation(float delta){
		Quaternion currRot = Transform.Basis.GetRotationQuaternion();
		LookAt(GlobalPosition + Velocity, Vector3.Up);
		Quaternion targetRot = Transform.Basis.GetRotationQuaternion();
		Rotation = currRot.Slerp(targetRot, delta * 3).GetEuler();
	}

	private void _OnTimerTimeOut(){
		if(_randomVelocity == Vector3.Zero){
			_randomVelocity = Velocity.Normalized().Rotated(Vector3.Up, Mathf.RadToDeg(RandomGenerator.Instance.GetRandFloatInRange(-45f, 45f)));
		}
		else{
			_randomVelocity = Vector3.Zero;
		}
	}
	/// <summary>
	/// Called from <c>AnimationPlayer</c> at the first frame.
	/// </summary>
	public void SpawnFootPrintDecal(){
		if(IsActive){
			GoatFootPrintDecalController footPrint = Pool.GetPoolable("goat_print", GlobalPosition) as GoatFootPrintDecalController;
			footPrint.GlobalPosition = GlobalPosition;
			footPrint.GlobalRotation = GlobalRotation;
			footPrint.OnSpawn();
		}
	}
	public void OnHovered(Vector3 pos){
		return;
	}
	public void OnPressed(Vector3 pos){
		if(_canBleat){
			if(Pool is GoatPathManager goatPathManager){
				AudioStreamPlayer3D ap = AudioManager.Instance.PlaySound(goatPathManager.goatAudioLibrary.GetSound("goat_bleating"));
				if(ap != null){
					_canBleat = false;
					_tempAudioStreamPlayer = ap;
					_tempAudioStreamPlayer.Finished += _OnBleatFinished;
				}
			}			
		}
		if(GlobalHelper.Instance.GameController.currPlayer is RealPlayerController player){
			player.EmitSignal(BasePlayerController.SignalName.GoatClicked, this);
		}
		
	}
	private void _OnBleatFinished(){
		_tempAudioStreamPlayer.Finished -= _OnBleatFinished;
		_tempAudioStreamPlayer = null;
		_canBleat = true;
	}
	public void OnReleased(Vector3 pos){
		return;
	}
	public void OnPressStopped(Vector3 pos){
		return;
	}
	public Vector3 GetGlobalPos(){
		return GlobalPosition;
	}
}
