using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class PiecePositionGeneratorTool : Node3D
{
    [Export] public bool AllowRun = false; 
    private bool _getPositions = false;
    [Export] public bool GetPositions {
        get { return _getPositions; }
        set {
            _getPositions = false;
            if(AllowRun){
                GetPositionsFromMarkers();
            }
            
        }
    }
    [Export] public PiecePositions positions;

    private void GetPositionsFromMarkers(){
        Godot.Collections.Array<Node> children = GetChildren();
        
        List<Marker3D> markers = new();

        foreach (Node child in children)
        {
            if(child is Marker3D marker){
                markers.Add(marker);
            }
        }
        GD.Print(markers.Count);
        positions = new();
        
        foreach (Marker3D marker in markers)
        {
            positions.AddPosToPositions(new PiecePosition(marker.GlobalPosition));
        }

    }
}