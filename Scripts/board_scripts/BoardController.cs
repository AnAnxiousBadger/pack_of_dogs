using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BoardController : Node3D
{
	// EXPORTS
	[Export] public AudioLibrary boardControllerAudioLibrary;
	/*[Export]*/ private PiecePositions _pieceStartingPositions;
	[Export] private PiecePositionListResource[] _pieceStartingPosesLists;
	/*[Export]*/ private PiecePositions _pieceEndPositions;
	[Export] private PiecePositionListResource[] _pieceEndPosesLists;
	[Export] public BoardElementsController boardElementsController;
	[Export] private PiecePathController _pieceMovingPath;
	[Export] private PiecePathController _pieceKickingpath;
	// OTHER
	public List<PieceController> pieces = new();
	private List<BoardNodeController> _nodes = new();
	private List<StartNodeController> _startNodes = new();
	//private Queue<(BoardNodeController, PieceController)> _nodesToDoOnStepActions = new();

	public void ReadyBoardController(){
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
		
		_pieceStartingPositions = new PiecePositions(_pieceStartingPosesLists);
		_pieceEndPositions = new PiecePositions(_pieceEndPosesLists);
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
			pieces[i].player = GlobalClassesHolder.Instance.GameController.players[pieces[i].playerIndex];
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

	public List<BoardNodeController> MoveForwardAlongNodesFromNode(BoardNodeController startNode, int steps, int playerIndex, bool isForSearching){
		return MoveAlongNodesFromNode(startNode, node => node.outNeighbours.ToList(), steps, playerIndex, isForSearching);
	}
	public List<BoardNodeController> MoveBackwardsAlongNodesFromNode(BoardNodeController startNode, int steps, int playerIndex, bool isForSearching){
		return MoveAlongNodesFromNode(startNode, node => node.inNeighbours, steps, playerIndex, isForSearching);
	}

	private List<BoardNodeController> MoveAlongNodesFromNode(BoardNodeController startNode, Func<BoardNodeController, List<BoardNodeController>> neighbours, int steps, int playerIndex, bool isForSearching){
		List<BoardNodeController> finalNodes = new();
		int remainingSteps = steps;
		List<BoardNodeController> currNodes = new() { startNode };
		while(remainingSteps > 0){
			List<BoardNodeController> nextNodes = new();
			for (int i = 0; i < currNodes.Count; i++)
			{
				for (int j = 0; j < neighbours(currNodes[i]).Count; j++)
				{
					if(playerIndex == -1 || neighbours(currNodes[i])[j].playerIndex == -1 || neighbours(currNodes[i])[j].playerIndex == playerIndex){
						nextNodes.Add(neighbours(currNodes[i])[j]);
					}
					
				}
			}
			currNodes = nextNodes;
			remainingSteps -= 1;
		}
		finalNodes = currNodes;
		finalNodes.Remove(startNode);
		finalNodes.RemoveAll(node => node.playerIndex != -1 && node.playerIndex != playerIndex);
		if(!isForSearching){	
			finalNodes.RemoveAll(node => node.currPieces.Any(piece => piece.playerIndex == playerIndex));
			finalNodes.RemoveAll(node => !node.canBeSteppedOn);
		}
		return finalNodes;
	}

	private List<BoardNodeController> BreadthFirstSearchFromNode(BoardNodeController startNode, Func<BoardNodeController, List<BoardNodeController>> neighbours, BoardNodeController destinationNode, int playerIndex){
		if(playerIndex != -1){
			if((startNode.playerIndex != -1 && startNode.playerIndex != playerIndex) || (destinationNode.playerIndex != -1 && destinationNode.playerIndex != playerIndex)){
				throw new ArgumentException("Destination cannot be reached!");
			}
		}
		
		BFSNode destination = null;
		List<BoardNodeController> exploredNodes = new();

		Queue<BFSNode> explorationQueue = new();
		explorationQueue.Enqueue(new BFSNode(startNode, null));
		while(explorationQueue.Count > 0){
			BFSNode current = explorationQueue.Dequeue();
			exploredNodes.Add(current.Node);
			if (current.Node == destinationNode){
				destination = current;
				break;
			}
			foreach (BoardNodeController neighbour in neighbours(current.Node))
			{
				if(!exploredNodes.Contains(neighbour)){
					explorationQueue.Enqueue(new BFSNode(neighbour, current));
				}
				
			}
		}

		if(destination == null){
			throw new ArgumentException("Destination could be reached!");
		}

		List<BoardNodeController> path = new();
		BFSNode currentTrace = destination;
		while(currentTrace.Parent != null){
			path.Add(currentTrace.Node);
			currentTrace = currentTrace.Parent;
		}

		path.Reverse();

		return path;

	}
	private List<BoardNodeController> BreadthFirstSearchFromNodeBackwards(BoardNodeController startNode, BoardNodeController destinationNode, int playerIndex){
		return BreadthFirstSearchFromNode(startNode, node => node.inNeighbours, destinationNode, playerIndex);
	}
	private List<BoardNodeController> BreadthFirstSearchFromNodeForward(BoardNodeController startNode, BoardNodeController destinationNode, int playerIndex){
		return BreadthFirstSearchFromNode(startNode, node => node.inNeighbours, destinationNode, playerIndex);
	}

	public int GetDistanceFromStartNode(BoardNodeController node,  BasePlayerController player){
		return BreadthFirstSearchFromNodeBackwards(node, GetStartNode(player), player.playerIndex).Count;
	}

	/*public void ResetBoard(){
		// Only reset piece starting positions for now
		/*for (int i = 0; i < _pieceStartingPositions; i++)
		{
			
		}
	}*/
}
