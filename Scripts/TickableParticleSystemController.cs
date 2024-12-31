using Godot;
using System;

public partial class TickableParticleSystemController : GpuParticles3D
{
	public enum SignalType {PRESSED, RELEASED, HOVERED, ENABLE, DISABLE}

	// EXPORTS
	[Export] public SignalType signalType;
	[Signal] public delegate void FinishedEmittingEventHandler(TickableParticleSystemController ps);

    public override void _Ready()
    {
		Finished += _OnFinishedEmitting;
    }

	private void _OnFinishedEmitting(){
		EmitSignal(SignalName.FinishedEmitting, this);
	}

}
