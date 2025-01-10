using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class BoardNodeController : StaticBody3D
{
	// Exports
	[Export] public int playerIndex = -1; // -1 for shared nodes
    [Export] public BoardNodeController[] inNeighbours = Array.Empty<BoardNodeController>();
	[Export] public BoardNodeController[] outNeighbours = Array.Empty<BoardNodeController>();
	// REFERENCES
	protected GameController _gameController;
	protected MeshInstance3D _mesh;
	// OTHER
	public bool canBeSteppedOn = true;
	public List<PieceController> currPieces;
	public Vector3 TopPos{
		get { return GlobalPosition + new Vector3(0f, 0.6f, 0f);}
	}
	// SIGNALS
	[Signal] public delegate void OnStepEventHandler(PieceController piece);
	[Signal] public delegate void OnleaveEventHandler(PieceController piece);
	
	public virtual void SetUpNode(GameController gameController){
		_gameController = gameController;
		_mesh = (MeshInstance3D)GetNode("node_mesh");
		currPieces = new();
	}
	public bool HasModifier(string name){
		List<BoardNodeModifier> mods = GetModifiers();
		for (int i = 0; i < mods.Count; i++)
		{
			if(mods[i].modifierName == name){
				return true;
			}
		}
		return false;
	}
	private List<BoardNodeModifier> GetModifiers(){
		List<BoardNodeModifier> modifiers = new();
		Godot.Collections.Array<Node> children = GetNode("modifiers").GetChildren();
		for (int i = 0; i < children.Count; i++)
		{
			if(children[i] is BoardNodeModifier mod){
				modifiers.Add(mod);
			}
		}
		return modifiers;
	}
	public List<PieceController> GetEnemyPieces(BasePlayerController ownPlayer){
		List<PieceController> enemies = new();
		for (int i = 0; i < currPieces.Count; i++)
		{
			if(currPieces[i].player != ownPlayer){
				enemies.Add(currPieces[i]);
			}
		}
		return enemies;
	}

	public List<BoardNodeController> MoveAlongNodesFromNode(int steps, int playerIndex, bool canMoveToOwnPiece){
		List<BoardNodeController> finalNodes = new();

		int remainingSteps = steps;
		List<BoardNodeController> currNodes = new() { this };
		while(remainingSteps > 0){
			List<BoardNodeController> nextNodes = new();
			for (int i = 0; i < currNodes.Count; i++)
			{
				for (int j = 0; j < currNodes[i].outNeighbours.Length; j++)
				{
					if(playerIndex == -1 || currNodes[i].outNeighbours[j].playerIndex == -1 || currNodes[i].outNeighbours[j].playerIndex == playerIndex){
						nextNodes.Add(currNodes[i].outNeighbours[j]);
					}
					
				}
			}
			currNodes = nextNodes;
			remainingSteps -= 1;
		}
		finalNodes = currNodes;
		if(!canMoveToOwnPiece){
			// Remove nodes where player already has a piece
			finalNodes.RemoveAll(node => node.currPieces.Any(piece => piece.playerIndex == playerIndex));
			// Remove nodes that cannot be stepped on
			finalNodes.RemoveAll(node => !node.canBeSteppedOn);
		}

		return finalNodes;
	}

	public abstract void Highlight();
	public abstract void RemoveHighlight();
	public abstract void DoOnLeaveNodeAction(PieceController piece);
	public abstract void DoOnStepNodeAction(PieceController piece);

}
