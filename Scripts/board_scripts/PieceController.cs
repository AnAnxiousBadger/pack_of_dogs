using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class PieceController : StaticBody3D
{
    [Export] public int playerIndex = 0;
    public BasePlayerController player;
    public BoardNodeController currNode = null;
    public bool hasArrived = false;
    private bool _isMoving = false;
    private Node3D _guideNode;
    private bool _isHighLit = false;

    public void HighlightPiece(bool allowTurnToMouse){
        AudioManager.Instance.PlaySound(GameController.Instance.boardController.boardControllerAudioLibrary.GetSound("picked_piece"), this, false);
        Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", new Vector3(Position.X, Position.Y + 1f, Position.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = allowTurnToMouse;
    }

    public void RemoveHighlight(){
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "position", new Vector3(Position.X, Position.Y - 1f, Position.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = false;
        Rotation = Vector3.Zero;
    }

    public void SubscribeToGuide(Node3D guideNode){
        AudioManager.Instance.PlaySound(GameController.Instance.boardController.boardControllerAudioLibrary.GetSound("piece_moved"), this, false);
        _isMoving = true;
        _isHighLit = false;
        Rotation = Vector3.Zero;
        _guideNode = guideNode;
    }
    public void UnsubscribeFromGuide(Vector3 targetPos){
        _guideNode = null;
        _isMoving = false;
        GlobalPosition = targetPos;
        HandleOnArrive(currNode);
    }

    public override void _PhysicsProcess(double delta)
    {
        if(_isMoving){
            GlobalPosition = _guideNode.GlobalPosition;
        }else if(_isHighLit){
            if(!GetViewport().GetCamera3D().IsPositionBehind(GlobalTransform.Origin)){
                Vector2 piecePosOnScreen = GetViewport().GetCamera3D().UnprojectPosition(GlobalTransform.Origin);
                Vector2 mousePos = GetViewport().GetMousePosition();
                Vector2 toMouse = mousePos - piecePosOnScreen;
                Rotation = new Vector3(Mathf.DegToRad(Mathf.Clamp(- 60f / 300f * toMouse.Length(), -60, 0)), toMouse.AngleTo(Vector2.Up), 0);
            }
        }
    }

    private void HandleOnArrive(BoardNodeController node){
        bool doKickEffect = false;
        List<PieceController> enemyPiecesOnNode = node.GetEnemyPieces(player);
		if(enemyPiecesOnNode.Count > 0){
            foreach (PieceController enemyPiece in enemyPiecesOnNode)
            {
                player.EmitSignal(BasePlayerController.SignalName.EnemyPieceHit);
                enemyPiece.player.EmitSignal(BasePlayerController.SignalName.PieceHit);
                BoardNodeController startNode = GameController.Instance.boardController.GetStartNode(enemyPiece.player);
                GameController.Instance.boardController.MovePiece(enemyPiece, startNode, true);
                doKickEffect = true;
            } 
		}
        node.DoOnStepNodeAction(this);
        node.ChainOnStepModifiers(this, doKickEffect);
    }

    public bool CanPieceMove(int roll){
        //List<BoardNodeController> possibleDestination = currNode.MoveForwardAlongNodesFromNode(roll, playerIndex, false);
        List<BoardNodeController> possibleDestination = GameController.Instance.boardController.MoveForwardAlongNodesFromNode(currNode, roll, playerIndex, false);
        if(possibleDestination.Count == 0){
            return false;
        }
        else{
            return true;
        }
    }
}