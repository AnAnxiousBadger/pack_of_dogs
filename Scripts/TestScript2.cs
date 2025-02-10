using Godot;
using System;

public partial class TestScript2 : Node
{
    public static TestScript TestScriptInstance { get; set; }

    public override void _Ready()
    {
        TestScriptInstance = null;
    }
}