using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BoardController : Node3D
{
	// EXPORTS
	[Export] private PiecePositions _pieceStartingPositions;
	[Export] private PiecePositions _pieceEndPositions;
	[Export] public BoardElementsController boardElementsController;
	[Export] private PiecePathController _pieceMovingPath;
	[Export] private PiecePathController _pieceKickingpath;
	// REFERENCES
	//public GameController gameController;
	// OTHER
	public List<PieceController> pieces = new();
	private List<BoardNodeController> _nodes = new();
	private List<StartNodeController> _startNodes = new();
	private Queue<(BoardNodeController, PieceController)> _nodesToDoOnStepActions = new();

	public void ReadyBoardController(){
		//this.gameController = gameController;
		SetUpBoard();
	}
	
	private void SetUpBoard(){
		// Get all pieces
		Godot.Collections.Array<Node> pieceContainerChildren = GetNode("pieces_container").GetChildren();
		
		for (int i = 0; i < pieceContainerChildren.Count; i++)
		{
			pieces.Add((PieceController)pieceContainerChildren[i]);
		}
		
		// Get all nodes
		Godot.Collections.Array<Node> nodeContainerChildren = GetNode("node_container").GetChildren();
		
		for (int i = 0; i < nodeContainerChildren.Count; i++)
		{
			if(nodeContainerChildren[i] is BoardNodeController node){
				_nodes.Add(node);
				node.SetUpNode();
				// Get start nodes
				if(node is StartNodeController startNode){
					_startNodes.Add(startNode);
				}
			}
		}
		
		for (int i = 0; i < pieces.Count; i++)
		{
			// Pair start nodes and pieces
			for (int j = 0; j < _startNodes.Count; j++)
			{
				if(pieces[i].playerIndex == _startNodes[j].playerIndex){
					pieces[i].currNode = _startNodes[j];
					_startNodes[j].currPieces.Add(pieces[i]);
				}
			}
			// Set pieces' initial positions
			pieces[i].Position = _pieceStartingPositions.TakeFreePosition(pieces[i]);
			pieces[i].hasArrived = false;
			CollisionShape3D shape = (CollisionShape3D) pieces[i].GetChild(1);
        	shape.Disabled = false;
			// Pair pieces and players
			pieces[i].player = GameController.Instance.players[pieces[i].playerIndex];
			pieces[i].player.pieces.Add(pieces[i]);
			// Set winning condition
			pieces[i].player.piecesToDeliver += 1;
		}
	}

	public void PieceLeavesStartPos(PieceController piece){
		_pieceStartingPositions.FreeUpPosition(piece);
	}
	public Vector3 GetPieceEndPos(PieceController piece){
		return _pieceEndPositions.TakeFreePosition(piece);
	}
	public StartNodeController GetStartNode(BasePlayerController player){
		for (int i = 0; i < _startNodes.Count; i++)
		{
			if(_startNodes[i].player == player){
				return _startNodes[i];
			}
			
		}
		return null;
	}
	public void MovePiece(PieceController piece, BoardNodeController node, bool isKicked){
		piece.currNode.DoOnLeaveNodeAction(piece);

		if(isKicked){
			_pieceKickingpath.SetUpPiecePath(piece.GlobalPosition, ToGlobal(_pieceStartingPositions.TakeFreePosition(piece)), piece);
		}
		else{
			_pieceMovingPath.SetUpPiecePath(piece.GlobalPosition, node.TopPos, piece);
		}
		
        piece.currNode.currPieces.Remove(piece);
        piece.currNode = node;
        node.currPieces.Add(piece);
    }
}
