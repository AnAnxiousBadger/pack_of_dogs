using System;
using Godot;

public partial class EndNodeController : BoardNodeController
{   
    private enum MeshStereo {L, R, LR};
    [Export] private MeshStereo _stereo;
    private VisualEffectController _hihglightEffectController;
    public override void DoOnLeaveNodeAction(PieceController piece){}
    public override void DoOnStepNodeAction(PieceController piece)
    {
        piece.hasArrived = true;
        currPieces.Remove(piece);
        CollisionShape3D pieceCollisionShape = (CollisionShape3D) piece.GetChild(1);
        pieceCollisionShape.Disabled = true;

        // INCREASE POINTS
        piece.player.DeliveredPieces += 1; 
    }

    public override void Highlight()
    {
        _hihglightEffectController = GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect($"end_node_{_stereo}_highlight_visual_effect", GlobalPosition, GlobalRotation);
    }

    public override void RemoveHighlight()
    {
        _hihglightEffectController?.EndEffect();
    }
}