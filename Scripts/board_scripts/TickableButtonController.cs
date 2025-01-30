using Godot;
using System;

public abstract partial class TickableButtonController : StaticBody3D, ITickable
{
    public bool IsActive {get; set;}
    [Export] public TickableEffect[] Effects {get; set;}
	[Export] protected AnimationPlayer _anim;
    public virtual void OnHovered(Vector3 pos){}
    public virtual void OnPressed(Vector3 pos){}
    public virtual void OnReleased(Vector3 pos){}
    public virtual void OnPressStopped(){}
    public Vector3 GetGlobalPos(){
        return GlobalPosition;
    }
    protected virtual void DisAbleButton(){}
	protected virtual void EnableButton(){}
    protected void ChangeActivity(bool active){
		if(active != IsActive){
            if(active){
                EnableButton();
            }
            else{
                DisAbleButton();
            }
        }
        
        IsActive = active;
	}
    protected virtual void DoButtonReleaseAction(){}
    protected void _OnButtonPressedWithoutClicking(BasePlayerController p){
		DoButtonReleaseAction();
	}
}