using Godot;
using System;

public partial class StartNodeController : BoardNodeController
{
    public BasePlayerController player;
    public override void SetUpNode()
    {
        base.SetUpNode();
        player = GlobalHelper.Instance.GameController.players[playerIndex];
    }
    public override void DoOnLeaveNodeAction(PieceController piece){
        GlobalHelper.Instance.GameController.boardController.PieceLeavesStartPos(piece);
    }
    public override void DoOnStepNodeAction(PieceController piece){}

    public override void Highlight(){}

    public override void RemoveHighlight(){}
}