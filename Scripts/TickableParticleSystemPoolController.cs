using System;
using System.Collections;
using Godot;
using System.Collections.Generic;

public partial class TickableParticleSystemPoolController : Node
{
    private List<TickableParticleSystemQueue> _queues;
    
    public override void _Ready()
    {
        // CERATE POOLS
        _queues = new();
        Godot.Collections.Array<Node> children = GetChildren();

        for (int i = 0; i < children.Count; i++)
        {
            if(children[i] is TickableParticleSystemController ps){
                List<TickableParticleSystemController> particleSystems = new() { ps };
                for (int j = 0; j < 4; j++)
                {
                    TickableParticleSystemController dupe = (TickableParticleSystemController)ps.Duplicate();
                    particleSystems.Add(dupe);
                    AddChild(dupe);
                }
                TickableParticleSystemQueue q = new(particleSystems);
                _queues.Add(q);
            }
        }

        // SUBSCRIBE TO SIGNALS
        TickableController parent = (TickableController) GetParent();
        parent.OnPressedTickable += _OnPressed;
    }

    private void _OnPressed(Vector3 pos){
        for (int i = 0; i < _queues.Count; i++)
        {
            if(_queues[i].signalType == TickableParticleSystemController.SignalType.PRESSED){
                _queues[i].EmitParticles(pos);
            }
        }
    }

}