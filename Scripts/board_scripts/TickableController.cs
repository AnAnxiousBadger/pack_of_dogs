using Godot;
using System;

public partial class TickableController : StaticBody3D
{
	public bool isActive = true;
	[Export] private TickableEffect[] _effects;
	// SIGNALS
	[Signal] public delegate void OnHoveredTickableEventHandler(Vector3 pos);
	[Signal] public delegate void OnPressedTickableEventHandler(Vector3 pos, bool isActive);
	[Signal] public delegate void OnReleasedTickableEventHandler(Vector3 pos, bool isActive);

	[Signal] public delegate void OnPressTickableStoppedEventHandler(bool isActive);
	[Signal] public delegate void OnActivityChangedEventHandler();

    public override void _Ready(){}

    public void Hover(Vector3 pos){
		EmitSignal(SignalName.OnHoveredTickable, pos);
	}

	public void Press(Vector3 pos){
		if(isActive){
			PlayVisualEffect(TickableEffect.SignalType.PRESSED, pos);
		}
		EmitSignal(SignalName.OnPressedTickable, pos, isActive);
	}
	public void Release(Vector3 pos){
		if(isActive){
			PlayVisualEffect(TickableEffect.SignalType.RELEASED, pos);
		}
		EmitSignal(SignalName.OnReleasedTickable, pos, isActive);
	}
	public void PressStopped(){
		EmitSignal(SignalName.OnPressTickableStopped, isActive);
	}

	public void PlayVisualEffect(TickableEffect.SignalType signalType, Vector3 clickPos){
		for (int i = 0; i < _effects.Length; i++)
		{
			if(signalType == _effects[i].signaltype){
				Vector3 pos;
				if(_effects[i].onClickPosition){
					pos = clickPos;
				}
				else{
					pos = GlobalPosition;
				}
				GameController.Instance.visualEffectPool.PlayVisualEffect(_effects[i].effectName, pos);
			}
		}
	}
}
