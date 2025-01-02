using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BoardController : Node3D
{
	// EXPORTS
	[Export] private PieceStartingPositions _pieceStartingPositions;
	[Export] public Label3D[] _endlabels;
	// REFERENCES
	private GameController _gameController;
	// OTHER
	public List<PieceController> pieces = new();
	private List<BoardNodeController> _nodes = new();
	private List<StartNodeController> _startNodes = new();

	public void ReadyBoardController(GameController gameController){
		_gameController = gameController;
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
				node.SetUpNode(_gameController);
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
			pieces[i].Position = TakeFreeStartingPosition(pieces[i]);
			pieces[i].hasArrived = false;
			CollisionShape3D shape = (CollisionShape3D) pieces[i].GetChild(1);
        	shape.Disabled = false;
			// Pair pieces and players
			pieces[i].player = _gameController.players[pieces[i].playerIndex];
			// Set winning condition
			pieces[i].player.piecesToDeliver += 1;
		}
	}

	public Vector3 TakeFreeStartingPosition(PieceController piece){
		for (int i = 0; i < _pieceStartingPositions.startingPositions.Length; i++)
		{
			PieceStartingPosition piecePos = _pieceStartingPositions.startingPositions[i];
			if(piecePos.playerIndex == piece.playerIndex && !piecePos.isOccupied){
				piecePos.isOccupied = true;
				piecePos.piece = piece;
				return piecePos.pos;
			}
		}
		return Vector3.Zero;
	}
	public void FreeStartingPosition(PieceController piece){
		for (int i = 0; i < _pieceStartingPositions.startingPositions.Length; i++)
		{
			PieceStartingPosition piecePos = _pieceStartingPositions.startingPositions[i];
			if(piecePos.piece == piece){
				piecePos.isOccupied = false;
				piecePos.piece = null;
				break;
			}
		}
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
	public void MovePiece(PieceController piece, BoardNodeController node){
		piece.currNode.DoOnLeaveNodeAction(piece);

        piece.currNode.currPieces.Remove(piece);
        piece.currNode = node;
        node.currPieces.Add(piece);

		node.DoOnStepNodeAction(piece);
    }

	public void SetplayerScoreLabel(BasePlayerController player){
		_endlabels[player.playerIndex].Text = player.DeliveredPieces.ToString();
	}
}
