using Godot;
using System;

public partial class GhostController : CritterController
{
    protected override void EmitCriterClickedSignal()
    {
        return;
    }
}