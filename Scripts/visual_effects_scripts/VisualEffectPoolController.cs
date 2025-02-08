using Godot;
using System;
using System.Collections.Generic;

public partial class VisualEffectPoolController : Node3D, IPoolManager
{
    [Export] public PoolableSettings[] PoolableScenes { get; set; }
    public Dictionary<string, Queue<IPoolable>> PoolablesDict { get; set; }
    public override void _Ready()
    {
        GameController.Instance.visualEffectPool = this;
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
    //private Dictionary<string, Queue<VisualEffectController>> _visualEffectDict = new();
    /*public override void _Ready()
    {
        // CREATE QUEUES
        Godot.Collections.Array<Node> children = GetChildren();

        for (int i = 0; i < children.Count; i++)
        {
            if(children[i] is VisualEffectController effect){
                effect.pool = this;
                Queue<VisualEffectController> effects = new();
                effects.Enqueue(effect);
                for (int j = 0; j < effect.poolCount - 1; j++)
                {
                    VisualEffectController dupe = (VisualEffectController)effect.Duplicate();
                    dupe.pool = this;
                    effects.Enqueue(dupe);
                    AddChild(dupe);
                }
                _visualEffectDict.Add(effect.effectName, effects);
            }
        }
    }*/

    /*public VisualEffectController PlayVisualEffect(string effectName, Vector3 globalPos){
        VisualEffectController effect = _visualEffectDict[effectName].Dequeue();

        effect.Play(globalPos);
        return effect;
    }

    public void ReQueueEffect(VisualEffectController effect){
        effect.GlobalPosition = GlobalPosition;
        _visualEffectDict[effect.effectName].Enqueue(effect);
    }*/
}