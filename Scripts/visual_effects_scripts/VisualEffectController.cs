using Godot;
using System;

public partial class VisualEffectController : Node3D
{
	[Export] public string effectName;
	[Export] public int poolCount;
	public VisualEffectPoolController pool;
	public virtual void Play(Vector3 globalPos){
		GlobalPosition = globalPos;
		Visible = true;
	}

	public virtual void EndEffect(){
		Visible = false;
		pool.ReQueueEffect(this);
	}
}
