using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class TickableParticleSystemController : GpuParticles3D
{
	public enum SignalType {PRESSED, RELEASED, ACTIVITY_CHANGED}

	// EXPORTS
	private SignalType _emittingSignal;
	[Export] public SignalType EmittingSignal {
		get { return _emittingSignal;}
		set {
				if(value == SignalType.ACTIVITY_CHANGED){
					IsPositionDependent = false;
				}
				_emittingSignal = value;
			}
	}
	private bool _isPositionDependent = false;
	[Export] public bool IsPositionDependent {
		get { return _isPositionDependent;}
		set {
			if(EmittingSignal == SignalType.ACTIVITY_CHANGED){
				_isPositionDependent = false;
			}
			else{
				_isPositionDependent = value;
			}
		}
	}
	// SIGNALS
	[Signal] public delegate void FinishedEmittingEventHandler(TickableParticleSystemController ps);

    public override void _Ready()
    {
		Finished += _OnFinishedEmitting;
    }

	private void _OnFinishedEmitting(){
		EmitSignal(SignalName.FinishedEmitting, this);
	}

}
