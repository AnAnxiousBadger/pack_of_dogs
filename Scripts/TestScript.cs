using Godot;
using System;

public partial class TestScript : Node
{
	
    public override void _EnterTree()
    {
        TestScript2.TestScriptInstance = this;
    }
    public override void _Ready()
    {
        GD.Print(TestScript2.TestScriptInstance.Name);
    }
    public override void _ExitTree(){
		TestScript2.TestScriptInstance = null;
	}
}
