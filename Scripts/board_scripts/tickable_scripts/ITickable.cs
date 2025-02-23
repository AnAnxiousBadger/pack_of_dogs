using Godot;
using System;
using System.Collections.Generic;

public interface ITickable
{
    public enum SignalType {PRESSED, RELEASED, HOVERED, ENABLE, DISABLE}
    bool IsActive { get; set;}
    Node3D TickableNode { get; }
    TickableVisualEffect[] Effects {get; set;}
    AudioLibrary AudioLibrary { get; set;}
    //TickableSoundEffect[] SoundEffects {get; set;}
    /// <summary>
    /// Interface method that should be called when the ITickable is hovered by the cursor.
    /// </summary>
    /// <param name="pos">Position where the raycast from the cursor hit the PhysicsBody3D.</param>
    public void OnHovered(Vector3 pos);
    public void PlayHoveredEffects(Vector3 pos){
        PlayVisualEffect(SignalType.HOVERED, pos);
        PlaySoundEffect(SignalType.HOVERED);
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
            PlayVisualEffect(SignalType.PRESSED, pos);
            PlaySoundEffect(SignalType.PRESSED);
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
            PlayVisualEffect(SignalType.RELEASED, pos);
            PlaySoundEffect(SignalType.RELEASED);
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
    public void PlayVisualEffect(SignalType signalType, Vector3 clickPos){
        foreach (TickableVisualEffect effect in Effects)
        {
            if(effect.allowOnDisabledTickableButtonClicks || (!effect.allowOnDisabledTickableButtonClicks && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons)){
                if(signalType == effect.signaltype){
                    Vector3 pos;
                    if(effect.onClickPosition){
                        pos = clickPos;
                    }
                    else{
                        pos = GetGlobalPos();
                    }
                    GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect(effect.effectName, pos);
                }
            }
        }
    }

    public void PlaySoundEffect(SignalType signalType){
        if(AudioLibrary != null){
            List<TickableSoundEffect> soundEffectsOfSignal = AudioLibrary.GetAllSoundEffectsOfType(s => s.signalType == signalType);
            foreach (TickableSoundEffect soundEffect in soundEffectsOfSignal)
            {
                if(soundEffect.allowOnDisabled || (!soundEffect.allowOnDisabled && IsActive && GlobalHelper.Instance.GameController.allowClicksOnTickableButtons)){
                    AudioManager.Instance.PlaySound(soundEffect.GetSoundData());
                }
            }
        }
    }
}