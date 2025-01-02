using System;
using System.Collections;
using Godot;
using System.Collections.Generic;

public partial class TickableParticleSystemPoolController : Node3D
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
        parent.OnReleasedTickable += _OnReleased;
        parent.OnActivityChanged += _OnActivityChanged;
    }

    private void _OnPressed(Vector3 pos){
        EmitParticlesOfSignal(TickableParticleSystemController.SignalType.PRESSED, pos);
    }

    private void _OnReleased(Vector3 pos){
        EmitParticlesOfSignal(TickableParticleSystemController.SignalType.RELEASED, pos);
    }
    private void _OnActivityChanged(){
        EmitParticlesOfSignal(TickableParticleSystemController.SignalType.ACTIVITY_CHANGED, Vector3.Zero);
    }
    private void EmitParticlesOfSignal(TickableParticleSystemController.SignalType type, Vector3 pos){
        for (int i = 0; i < _queues.Count; i++)
        {
            if(_queues[i].emittingSignal == type){
                _queues[i].EmitParticles(pos);
            }
        }
    }

}