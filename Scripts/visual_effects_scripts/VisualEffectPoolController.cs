using Godot;
using System;
using System.Collections.Generic;

public partial class VisualEffectPoolController : Node3D, IPoolManager
{
    [Export] public PoolableSettings[] PoolableScenes { get; set; }
    public Dictionary<string, Queue<IPoolable>> PoolablesDict { get; set; }
    public override void _Ready()
    {
        GlobalHelper.Instance.GameController.visualEffectPool = this;
        ((IPoolManager)this).SpawnPoolables(this);
    }
    public IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos)
    {
        if(poolable is VisualEffectController effect){
            effect.Play(pos);
            return poolable;
        }
        else{
            return null;
        } 
    }

    public void DoOnReQueueToPoolAction(IPoolable poolable)
    {
        if(poolable is VisualEffectController effect){
            effect.GlobalPosition = GlobalPosition;
        }
    }

    public VisualEffectController PlayVisualEffect(string effectName, Vector3 globalPos){
        if(((IPoolManager)this).GetPoolable(effectName, globalPos) is VisualEffectController effect){
            return effect;
        }
        else{
            return null;
        } 
    }
}