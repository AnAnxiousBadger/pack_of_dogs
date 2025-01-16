using Godot;
using System;

public partial class VisualEffectController : Node3D
{
	[Export] public string effectName;
	[Export] public int poolCount;
	public VisualEffectPoolController pool;
	[Signal] public delegate void OnEffectEndedEventHandler(VisualEffectController effect);
	public virtual void Play(Vector3 globalPos){
		GlobalPosition = globalPos;
	}

	public virtual void EndEffect(){
		EmitSignal(SignalName.OnEffectEnded, this);
		pool.ReQueueEffect(this);
	}
}
