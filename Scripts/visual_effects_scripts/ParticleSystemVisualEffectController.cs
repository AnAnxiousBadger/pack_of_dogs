using Godot;
using System;
using System.Collections.Generic;

public partial class ParticleSystemVisualEffectController : VisualEffectController
{
    private GpuParticles3D _particles;
    public override void _Ready()
    {
        _particles = GetNode("particles") as GpuParticles3D;
        _particles.Finished += EndEffect;

    }
    public override void Play(Vector3 globalPos){
        base.Play(globalPos);
        _particles.Emitting = true;
    }
    public override void Play(Vector3 globalPos, Vector3 globalRot){
        base.Play(globalPos, globalRot);
        _particles.Emitting = true;
    }
}