using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class TickableParticleSystemQueue
{
    public Queue<TickableParticleSystemController> q;
    public TickableParticleSystemController.SignalType signalType;

    public TickableParticleSystemQueue(List<TickableParticleSystemController> psList){
        if(psList.Count > 0){
            signalType = psList[0].signalType;
        }

        q = new();
        for (int i = 0; i < psList.Count; i++)
        {
            q.Enqueue(psList[i]);
            psList[i].FinishedEmitting += EnqueueBackParticleSystem;
        }
    }

    public void EmitParticles(Vector3 pos){
        TickableParticleSystemController ps = q.Dequeue();
        ps.GlobalPosition = pos;
        ps.Emitting = true;
    }

    public void EnqueueBackParticleSystem(TickableParticleSystemController ps){
        q.Enqueue(ps);
    }
}