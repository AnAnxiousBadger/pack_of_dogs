using Godot;
using System;

public partial class GoatFootPrintDecalController : Node3D, IPoolable
{
	[Export] private Timer _disappearTimer;
	[Export] private float _disappearTime;
    public IPoolManager Pool { get; set; }
    public string Identifier { get; set; }
    public Node PoolableNode => this;

    public override void _Ready()
    {
		_disappearTimer.WaitTime = _disappearTime;
        _disappearTimer.Timeout += Disappear;
    }
    public void OnSpawn(){
        _disappearTimer.Start();
    }
	private void Disappear(){
        Pool.ReQueuePoolable(this);
	}
}
