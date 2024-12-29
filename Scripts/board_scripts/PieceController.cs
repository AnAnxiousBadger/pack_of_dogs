using Godot;
using System;

public partial class PieceController : StaticBody3D
{
    [Export] public int playerIndex = 0;
    public BasePlayerController player;
    public BoardNodeController currNode = null;
    public bool hasArrived = false;
}