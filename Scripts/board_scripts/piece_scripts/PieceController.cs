using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class PieceController : StaticBody3D
{
    // EXPORTS
    [ExportCategory("Common Exports")]
    [Export] public AudioLibrary audioLibrary;

    [ExportCategory("Per Instance Exports")]
    [Export] public int playerIndex = -1;

    // REFERENCES
    public BasePlayerController player;
    public BoardNodeController currNode = null;

    // OTHERS
    private Node3D _guideNode;
    public bool hasArrived = false;
    private bool _isSubscribed = false;
    private bool _isHighLit = false;
    public Vector3 OffsetFromGuide { get; set;} = Vector3.Zero;
    
    public override void _PhysicsProcess(double delta)
    {
        if(_isSubscribed){
            GlobalPosition = _guideNode.GlobalPosition + OffsetFromGuide;
        }
        else if(_isHighLit){
            
            if(!GetViewport().GetCamera3D().IsPositionBehind(GlobalTransform.Origin)){
                Vector2 piecePosOnScreen = GetViewport().GetCamera3D().UnprojectPosition(GlobalTransform.Origin);
                Vector2 mousePos = GetViewport().GetMousePosition();
                Vector2 toMouse = mousePos - piecePosOnScreen;
                Rotation = new Vector3(Mathf.DegToRad(Mathf.Clamp(- 60f / 300f * toMouse.Length(), -60, 0)), toMouse.AngleTo(Vector2.Up), 0);
            }
        }
    }
    public void HighlightPiece(bool allowRotationToMouse){
        AudioManager.Instance.PlaySound(audioLibrary.GetSound("piece_picked"));
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "OffsetFromGuide", new Vector3(OffsetFromGuide.X, OffsetFromGuide.Y + 1f, OffsetFromGuide.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = allowRotationToMouse;
    }

    public void RemoveHighlight(){
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "OffsetFromGuide", new Vector3(OffsetFromGuide.X, OffsetFromGuide.Y - 1f, OffsetFromGuide.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = false;
        Rotation = Vector3.Zero;
    }

    public void SubscribeToGuide(Node3D guideNode, Vector3 offset){
        OffsetFromGuide = offset;
        _isSubscribed = true;
        _isHighLit = false;
        Rotation = Vector3.Zero;
        _guideNode = guideNode;
    }
    public void UnsubscribeFromGuide(Vector3 targetPos){
        _guideNode = null;
        _isSubscribed = false;
        GlobalPosition = targetPos;
        HandleOnArrive(currNode);
    }

    private void HandleOnArrive(BoardNodeController node){
        // SUBSCRIBE TO NODE'S GUIDE
        SubscribeToGuide(node.topGuide, GlobalPosition - node.topGuide.GlobalPosition);

        // HANDLE ON STEP ACTIONS
        bool doKickEffect = false;
        List<PieceController> enemyPiecesOnNode = node.GetEnemyPieces(player);
		if(enemyPiecesOnNode.Count > 0){
            foreach (PieceController enemyPiece in enemyPiecesOnNode)
            {
                player.EmitSignal(BasePlayerController.SignalName.EnemyPieceHit);
                enemyPiece.player.EmitSignal(BasePlayerController.SignalName.PieceHit);
                BoardNodeController startNode = GlobalHelper.Instance.GameController.boardController.GetStartNode(enemyPiece.player);
                GlobalHelper.Instance.GameController.boardController.MovePiece(enemyPiece, startNode, true);
                doKickEffect = true;
            } 
		}
        node.DoOnStepNodeAction(this);
        node.ChainOnStepModifiers(this, doKickEffect);
    }

    public bool CanPieceMove(int roll){
        List<BoardNodeController> possibleDestination = GlobalHelper.Instance.GameController.boardController.MoveForwardAlongNodesFromNode(currNode, roll, playerIndex, false);
        if(possibleDestination.Count == 0){
            return false;
        }
        else{
            return true;
        }
    }
}