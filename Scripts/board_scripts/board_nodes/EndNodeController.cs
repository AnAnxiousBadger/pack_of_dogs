using System;
using Godot;

public partial class EndNodeController : BoardNodeController
{
    public override void DoOnLeaveNodeAction(PieceController piece){}
    public override void DoOnStepNodeAction(PieceController piece)
    {
        // HIDE PIECE
        piece.hasArrived = true;
        currPieces.Remove(piece);
        piece.Visible = false;
        CollisionShape3D shape = (CollisionShape3D) piece.GetChild(1);
        shape.Disabled = true;

        // INCREASE POINTS
        piece.player.Score += 1;
        
    }

    public override void Highlight()
    {
        Material mat = GD.Load<Material>("res://Assets/Materials/orange_mat.tres");
        _mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public override void RemoveHighlight()
    {
        Material mat;
        if(playerIndex == 0){
            mat = GD.Load<Material>("res://Assets/Materials/white_mat.tres");
        }
        else{
            mat = GD.Load<Material>("res://Assets/Materials/black_mat.tres");
        }
        _mesh.SetSurfaceOverrideMaterial(0, mat);
    }
}