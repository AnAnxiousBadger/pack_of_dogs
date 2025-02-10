using Godot;
using System;

public partial class StartNodeController : BoardNodeController
{
    public BasePlayerController player;
    public override void SetUpNode()
    {
        base.SetUpNode();
        player = GlobalClassesHolder.Instance.GameController.players[playerIndex];
    }
    public override void DoOnLeaveNodeAction(PieceController piece){
        GlobalClassesHolder.Instance.GameController.boardController.PieceLeavesStartPos(piece);
    }
    public override void DoOnStepNodeAction(PieceController piece){}

    public override void Highlight(){}

    public override void RemoveHighlight(){}
}