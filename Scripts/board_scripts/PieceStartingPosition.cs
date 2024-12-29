using Godot;
using System;

[GlobalClass]
public partial class PieceStartingPosition: Resource
{
    [Export] public int playerIndex;
    [Export] public Vector3 pos;
    [Export] public bool isOccupied;
    public PieceController piece;

    public PieceStartingPosition(){
        this.playerIndex= 0;
        this.pos = Vector3.Zero;
        this.isOccupied = false;
        this.piece = null;
    }
}