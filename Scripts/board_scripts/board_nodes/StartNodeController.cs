using Godot;
using System;

public partial class StartNodeController : BoardNodeController
{
    public BasePlayerController player;
    public override void SetUpNode(GameController gameController)
    {
        base.SetUpNode(gameController);
        player = GameController.Instance.players[playerIndex];
    }
    public override void DoOnLeaveNodeAction(PieceController piece){
        GameController.Instance.boardController.PieceLeavesStartPos(piece);
    }
    public override void DoOnStepNodeAction(PieceController piece){}

    public override void Highlight()
    {
        
    }

    public override void RemoveHighlight()
    {
        
    }
}