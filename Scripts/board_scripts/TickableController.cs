using Godot;
using System;

/*public partial class TickableController : StaticBody3D
{
	protected bool isActive = true;
	
	[Export] private TickableEffect[] _effects;

    public virtual void OnHovered(Vector3 pos){}

	public virtual void OnPressed(Vector3 pos){
		if(isActive){
			PlayVisualEffect(TickableEffect.SignalType.PRESSED, pos);
		}
	}
	public virtual void OnReleased(Vector3 pos){
		if(isActive){
			PlayVisualEffect(TickableEffect.SignalType.RELEASED, pos);
		}
	}
	public virtual void OnPressStopped(){}

	protected void PlayVisualEffect(TickableEffect.SignalType signalType, Vector3 clickPos){
		for (int i = 0; i < _effects.Length; i++)
		{
			if(_effects[i].allowOnDisabledTickableButtonClicks || (!_effects[i].allowOnDisabledTickableButtonClicks && GameController.Instance.allowClicksOnTickableButtons)){
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
}*/
