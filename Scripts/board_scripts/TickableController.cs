using Godot;
using System;

public partial class TickableController : StaticBody3D
{
	// EXPORTS
	[Export] private BoardController _boardController;
	[Export] private bool _isActive = true;
	[Export] private TickableEffect[] _effects;
	public bool IsActive
	{
		get{ return _isActive; }
		set{
			_isActive = value;
			if(value && _hasEnableAnim){
				_anim.Play(_animLibrary + "/on_enable");
			}
			else if(!value && _hasDisableAnim){
				_anim.Play(_animLibrary + "/on_disable");
			}
			EmitSignal(SignalName.OnActivityChanged);
		}
	}
	// REFERENCES
	private AnimationPlayer _anim;
	// OTHER
	private bool _hasEnableAnim = false; // on_enable
	private bool _hasDisableAnim =  false; // on_disbale
	private bool _hasPressedAnim = false; // on_pressed
	private bool _hasPressedDisabledAnim = false; // on_disabled_pressed
	private bool _hasHoverAnim = false; // on_hover
	private bool _hasHoverDisabledAnim = false; // on_disabled_hover
	private string _animLibrary;
	// SIGNALS
	[Signal] public delegate void OnPressedTickableEventHandler(Vector3 pos);
	[Signal] public delegate void OnReleasedTickableEventHandler(Vector3 pos);
	[Signal] public delegate void OnActivityChangedEventHandler();

    public override void _Ready()
    {
        _anim = GetNode("AnimationPlayer") as AnimationPlayer;
		if(_anim?.GetAnimationLibraryList().Count > 0){
			_animLibrary = _anim.GetAnimationLibraryList()[0];
			_hasEnableAnim = _anim.HasAnimation(_animLibrary + "/on_enable");
			_hasDisableAnim = _anim.HasAnimation(_animLibrary + "/on_disable");
			_hasPressedAnim = _anim.HasAnimation(_animLibrary + "/on_pressed");
			_hasPressedDisabledAnim = _anim.HasAnimation(_animLibrary + "/on_disable_pressed");
			_hasHoverAnim = _anim.HasAnimation(_animLibrary + "/on_hover");
			_hasHoverDisabledAnim = _anim.HasAnimation(_animLibrary + "/on_disabled_hover");
		}

		IsActive = IsActive; // Needed so inactive tickables animate to their starting position
    }

    public void OnHover(){
		if(IsActive && _hasHoverAnim){
			_anim.Play(_animLibrary + "/on_hover");
		}
		else if(!IsActive && _hasHoverDisabledAnim){
			_anim.Play(_animLibrary + "/on_disabled_hover");
		}
	}

	public void OnPressed(Vector3 pos){
		if(IsActive){
			PlayVisualEffect(TickableEffect.SignalType.PRESSED, pos);
			if(_hasPressedAnim){
				_anim.Play(_animLibrary + "/on_pressed");
			}
		}
		else if(!IsActive){
			if(_hasPressedDisabledAnim){
				_anim.Play(_animLibrary + "/on_disable_pressed");
			}
		}
	}
	public void OnReleased(Vector3 pos){
		if(IsActive){
			PlayVisualEffect(TickableEffect.SignalType.RELEASED, pos);
			EmitSignal(SignalName.OnReleasedTickable, pos);
		}
	}
	public void OnHoverStopped(){
		if(IsActive && (!_anim.IsPlaying() || _anim.CurrentAnimation == _animLibrary + "/on_pressed")){
			_anim.Play(_animLibrary + "/RESET");
		}
	}

	private void PlayVisualEffect(TickableEffect.SignalType signalType, Vector3 clickPos){
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
				_boardController.gameController.visualEffectPool.PlayVisualEffect(_effects[i].effectName, pos);
			}
		}
	}
}
