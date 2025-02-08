using Godot;
using System;
public partial class PalmDisturbController : Node3D
{
	[Export] private Timer _returnTimer;

    public override void _Ready()
    {
        _returnTimer.Timeout += _OnReturnTimerTimeout;
    }
    public override void _Process(double delta)
	{
		RenderingServer.GlobalShaderParameterSet("palm_disturb_pos", GlobalPosition);
	}

	private void _OnReturnTimerTimeout(){
		GlobalPosition = new Vector3(0f, -10f, 0f);
	}

	public void Distrub(Vector3 pos){
		GlobalPosition = pos;
		_returnTimer.Start();
	}


}
