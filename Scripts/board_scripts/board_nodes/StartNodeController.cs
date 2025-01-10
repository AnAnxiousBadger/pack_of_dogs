using Godot;
using System;

public partial class StartNodeController : BoardNodeController
{
    public BasePlayerController player;
    public override void SetUpNode(GameController gameController)
    {
        base.SetUpNode(gameController);
        player = gameController.players[playerIndex];
    }
    public override void DoOnLeaveNodeAction(PieceController piece){
        _gameController.boardController.FreeStartingPosition(piece);
    }
    public override void DoOnStepNodeAction(PieceController piece){}

    public override void Highlight()
    {
        
    }

    public override void RemoveHighlight()
    {
        
    }
}