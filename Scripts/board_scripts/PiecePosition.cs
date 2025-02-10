using Godot;
using System;

[GlobalClass]
public partial class PiecePosition: Resource
{
    [Export] public int playerIndex;
    [Export] public Vector3 pos;
    [Export] public bool isOccupied;
    public PieceController piece;

    public PiecePosition(){
        this.playerIndex= 0;
        this.pos = Vector3.Zero;
        this.isOccupied = false;
        this.piece = null;
    }

    public PiecePosition(Vector3 pos){
        this.playerIndex= 0;
        this.pos = pos;
        this.isOccupied = false;
        this.piece = null;
    }
    public PiecePosition(int playerIndex, Vector3 pos){
        this.playerIndex = playerIndex;
        this.pos = pos;
        this.isOccupied = false;
        this.piece = null;
    }

    
}