using Godot;
using System;

public interface ITickable
{
    bool IsActive { get; set;}
    Node3D TickableNode { get; }
    TickableVisualEffect[] Effects {get; set;}
    TickableSoundEffect[] SoundEffects {get; set;}
    /// <summary>
    /// Interface method that should be called when the ITickable is hovered by the cursor.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void OnHovered(Vector3 pos);
    public void PlayHoveredEffects(Vector3 pos){
        PlayVisualEffect(TickableEffect.SignalType.HOVERED, pos);
        PlaySoundEffect(TickableEffect.SignalType.HOVERED);
    }
    /// <summary>
    /// Interface method that should be called when the ITickable is just pressed by the cursor.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void OnPressed(Vector3 pos);
    /// <summary>
    /// Explicit interface method that plays TickableEffects from Effects that are set to be played on cursor press.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void PlayPressedEffects(Vector3 pos){
        if(IsActive){
            PlayVisualEffect(TickableEffect.SignalType.PRESSED, pos);
            PlaySoundEffect(TickableEffect.SignalType.PRESSED);
        }
    }
    /// <summary>
    /// Interface method that should be called when the ITickable is just released by the cursor.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void OnReleased(Vector3 pos);
    /// <summary>
    /// Explicit interface method that plays TickableEffects from Effects that are set to be played on cursor release.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void PlayReleasedEffects(Vector3 pos){
        if(IsActive){
            PlayVisualEffect(TickableEffect.SignalType.RELEASED, pos);
            PlaySoundEffect(TickableEffect.SignalType.RELEASED);
        }
    }
    /// <summary>
    /// Interface method that should be called when the ITickable has been pressed and the cursor is still pressing but no longer on the PhysicsBody3D.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void OnPressStopped(){}
    /// <summary>
    /// Helper interface method for the ITickable.PlayVisualEffect.
    /// </summary>
    /// <returns>Should return the GlobalPosition of the PhysicsBody3D.</returns>
    protected Vector3 GetGlobalPos(); 
    /// <summary>
    /// Explicit interface method that plays all TickableEffects from the Effects array that are of signalType.
    /// </summary>
    /// <param name="signalType">The signalType of the TickableEffects to be played.</param>
    /// <param name="clickPos">The position of the cursor where the TickableEffect should be playing if the effect is position dependent.</param>
    public void PlayVisualEffect(TickableEffect.SignalType signalType, Vector3 clickPos){
        for (int i = 0; i < Effects.Length; i++)
        {
            if(Effects[i].allowOnDisabledTickableButtonClicks || (!Effects[i].allowOnDisabledTickableButtonClicks && GlobalClassesHolder.Instance.GameController.allowClicksOnTickableButtons)){
                if(signalType == Effects[i].signaltype){
                    Vector3 pos;
                    if(Effects[i].onClickPosition){
                        pos = clickPos;
                    }
                    else{
                        pos = GetGlobalPos();
                    }
                    GlobalClassesHolder.Instance.GameController.visualEffectPool.PlayVisualEffect(Effects[i].effectName, pos);
                }
            }
            
        }
    }

    public void PlaySoundEffect(TickableEffect.SignalType signalType){
        for (int i = 0; i < SoundEffects.Length; i++)
        {
            if(SoundEffects[i].signaltype == signalType){
                if(IsActive || SoundEffects[i].allowOnDisabled){
                    AudioManager.Instance.PlaySound(GlobalClassesHolder.Instance.GameController.boardController.boardElementsController.boardElementsAudioLibrary.GetSound(SoundEffects[i].effectName));
                }
                
            }
        }
    }
}