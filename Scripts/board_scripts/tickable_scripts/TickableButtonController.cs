using Godot;
using System;

public abstract partial class TickableButtonController : StaticBody3D, ITickable
{
    public bool IsActive {get; set;}
    public Node3D TickableNode {get  => this;}
    [Export] public TickableVisualEffect[] Effects {get; set;}
    [Export] public AudioLibrary AudioLibrary { get; set; }
	[Export] protected AnimationPlayer _anim;
    public virtual void OnHovered(Vector3 pos){}
    public virtual void OnPressed(Vector3 pos){}
    public virtual void OnReleased(Vector3 pos){}
    public virtual void OnPressStopped(){}
    public Vector3 GetGlobalPos(){
        return GlobalPosition;
    }
    protected virtual void DisableButton(){
        ITickable tickable = this;
        tickable.PlaySoundEffect(ITickable.SignalType.DISABLE);
        tickable.PlayVisualEffect(ITickable.SignalType.DISABLE, GlobalPosition);
    }
	protected virtual void EnableButton(){
        ITickable tickable = this;
        tickable.PlaySoundEffect(ITickable.SignalType.ENABLE);
        tickable.PlayVisualEffect(ITickable.SignalType.ENABLE, GlobalPosition);
    }
    protected void ChangeActivity(bool active){
		if(active != IsActive){
            if(active){
                EnableButton();
            }
            else{
                DisableButton();
            }
        }
        
        IsActive = active;
	}
    protected virtual void DoButtonReleaseAction(){}
    protected void _OnButtonPressedWithoutClicking(BasePlayerController p){
		DoButtonReleaseAction();
	}
}