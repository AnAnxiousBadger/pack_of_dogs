using Godot;
using System;
using System.Collections.Generic;

public partial class VisualEffectPoolController : Node3D
{

    private Dictionary<string, Queue<VisualEffectController>> visualEffectDict = new();
    public override void _Ready()
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
                visualEffectDict.Add(effect.effectName, effects);
            }
        }
    }
    /*public VisualEffectController PlayVisualEffect(string effectName){
        VisualEffectController effect = visualEffectDict[effectName].Dequeue();
        effect.Visible = true;
        effect.Play();
        return effect;
    }*/
    public VisualEffectController PlayVisualEffect(string effectName, Vector3 globalPos){
        VisualEffectController effect = visualEffectDict[effectName].Dequeue();
        effect.Visible = true;
        effect.Play(globalPos);
        return effect;
    }

    public void ReQueueEffect(VisualEffectController effect){
        visualEffectDict[effect.effectName].Enqueue(effect);
        effect.Visible = false;
    }
}