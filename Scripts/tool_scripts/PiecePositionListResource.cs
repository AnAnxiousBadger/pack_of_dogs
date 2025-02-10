using Godot;
using System;
[GlobalClass]
public partial class PiecePositionListResource : Resource
{
    [Export] public Vector3[] poses;

    public PiecePositionListResource(){
        this.poses = Array.Empty<Vector3>();
    }
    public PiecePositionListResource(Vector3[] vectors){
        this.poses = vectors;
    }
}