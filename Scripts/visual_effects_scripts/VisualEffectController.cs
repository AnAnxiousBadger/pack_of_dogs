using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class VisualEffectController : Node3D, IPoolable
{
	[Export] private BaseSoundEffect effectSound;
	public string Identifier { get; set; }
	public Node PoolableNode {
		get{
			return this;
		}
	}
	public IPoolManager Pool { get; set; }
	[Signal] public delegate void OnEffectEndedEventHandler(VisualEffectController effect);
	public virtual void Play(Vector3 globalPos){
		GlobalPosition = globalPos;
		if(effectSound != null){
			AudioManager.Instance.PlaySound(effectSound.GetSound());
		}
	}

	public virtual void EndEffect(){
		EmitSignal(SignalName.OnEffectEnded, this);
		Pool.ReQueuePoolable(this);
	}
}
