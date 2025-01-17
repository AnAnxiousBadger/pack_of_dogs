using Godot;
using System;
using System.Collections.Generic;

public partial class PieceController : StaticBody3D
{
    [Export] public int playerIndex = 0;
    public BasePlayerController player;
    public BoardNodeController currNode = null;
    public bool hasArrived = false;
    private bool _isMoving = false;
    private Node3D _guideNode;
    private bool _isHighLit = false;

    public void HighlightPiece(){
        Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", new Vector3(Position.X, Position.Y + 1f, Position.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = true;
    }

    public void RemoveHighlight(){
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "position", new Vector3(Position.X, Position.Y - 1f, Position.Z), 0.35f).SetTrans(Tween.TransitionType.Cubic);
        _isHighLit = false;
        Rotation = Vector3.Zero;
    }

    public void SubscribeToGuide(Node3D guideNode){
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
        PieceController enemyPieceOnNode = node.GetEnemyPiece(player);
		if(enemyPieceOnNode != null){
			player.EmitSignal(BasePlayerController.SignalName.EnemyPieceHit);
			enemyPieceOnNode.player.EmitSignal(BasePlayerController.SignalName.PieceHit);
			BoardNodeController startNode = GameController.Instance.boardController.GetStartNode(enemyPieceOnNode.player);
			GameController.Instance.boardController.MovePiece(enemyPieceOnNode, startNode, true);
            doKickEffect = true;
		}
        node.DoOnStepNodeAction(this);
        node.ChainOnStepModifiers(this, doKickEffect);
    }
}