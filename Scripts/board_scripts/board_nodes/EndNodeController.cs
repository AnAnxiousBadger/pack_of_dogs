using System;
using Godot;

public partial class EndNodeController : BoardNodeController
{   
    private enum MeshStereo {L, R};
    [Export] private MeshStereo stereo;
    public override Vector3 TopPos{
		get { return GlobalPosition + new Vector3(0f, 2f, 0f);}
	}
    private VisualEffectController hihglight;
    public override void DoOnLeaveNodeAction(PieceController piece){}
    public override void DoOnStepNodeAction(PieceController piece)
    {
        piece.hasArrived = true;
        currPieces.Remove(piece);
        CollisionShape3D shape = (CollisionShape3D) piece.GetChild(1);
        shape.Disabled = true;

        // INCREASE POINTS
        piece.player.DeliveredPieces += 1; 
    }

    public override void Highlight()
    {
        hihglight = GameController.Instance.visualEffectPool.PlayVisualEffect($"end_node_{stereo}_highlight_visual_effect", GlobalPosition);
    }

    public override void RemoveHighlight()
    {
        hihglight?.EndEffect();
    }
}