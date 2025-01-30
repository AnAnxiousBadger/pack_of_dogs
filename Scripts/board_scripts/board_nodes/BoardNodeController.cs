using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class BoardNodeController : StaticBody3D
{
	// Exports
	[Export] public int playerIndex = -1; // -1 for shared nodes
	[Export] public BoardNodeController[] outNeighbours = Array.Empty<BoardNodeController>();
	// REFERENCES
	public List<BoardNodeController> inNeighbours = new();
	protected MeshInstance3D _mesh;
	// OTHER
	public bool canBeSteppedOn = true;
	public List<PieceController> currPieces;
	public virtual Vector3 TopPos{
		get { return GlobalPosition + new Vector3(0f, 0.6f, 0f);}
	}
	private Queue<BoardNodeModifier> _onStepModQueue = new();
	private PieceController _modsQueuePiece;
	// SIGNALS
	[Signal] public delegate void OnLeaveEventHandler(PieceController piece); // SHOULD BE HANDLED LIKE ONSTEP
	[Signal] public delegate void OnAllOnStepModifierAppliedEventHandler(BoardNodeController node);
	
	public virtual void SetUpNode(){
		_mesh = (MeshInstance3D)GetNode("node_mesh");
		currPieces = new();
		// Set up in neighbours from given out neighbours
		foreach (BoardNodeController outNeighbour in outNeighbours)
		{
			outNeighbour.inNeighbours.Add(this);
		}
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

	//public List<BoardNodeController> MoveForwardAlongNodesFromNode(int steps, int playerIndex, bool canMoveToOwnPiece){
		/*List<BoardNodeController> finalNodes = new();

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

		return finalNodes;*/
		/*return MoveAlongNodesFromNode(node => node.outNeighbours.ToList(), steps, playerIndex, canMoveToOwnPiece);
	}
	public List<BoardNodeController> MoveBackwardsAlongNodesFromNode(int steps, int playerIndex, bool canMoveToOwnPiece){
		/*List<BoardNodeController> finalNodes = new();
		int remainingSteps = steps;
		List<BoardNodeController> currNodes = new() { this };
		while(remainingSteps > 0){
			List<BoardNodeController> nextNodes = new();
			for (int i = 0; i < currNodes.Count; i++)
			{
				for (int j = 0; j < currNodes[i].inNeighbours.Count; j++)
				{
					if(playerIndex == -1 || currNodes[i].inNeighbours[j].playerIndex == -1 || currNodes[i].inNeighbours[j].playerIndex == playerIndex){
						nextNodes.Add(currNodes[i].inNeighbours[j]);
					}
					
				}
			}
			currNodes = nextNodes;
			remainingSteps -= 1;
		}
		finalNodes = currNodes;
		if(!canMoveToOwnPiece){
			finalNodes.RemoveAll(node => node.currPieces.Any(piece => piece.playerIndex == playerIndex));
			finalNodes.RemoveAll(node => !node.canBeSteppedOn);
		}

		return finalNodes;*/
		/*return MoveAlongNodesFromNode(node => node.inNeighbours, steps, playerIndex, canMoveToOwnPiece);
	}

	private List<BoardNodeController> MoveAlongNodesFromNode(Func<BoardNodeController, List<BoardNodeController>> neighbourDirection, int steps, int playerIndex, bool canMoveToOwnPiece){
		List<BoardNodeController> finalNodes = new();
		int remainingSteps = steps;
		List<BoardNodeController> currNodes = new() { this };
		while(remainingSteps > 0){
			List<BoardNodeController> nextNodes = new();
			for (int i = 0; i < currNodes.Count; i++)
			{
				for (int j = 0; j < neighbourDirection(currNodes[i]).Count; j++)
				{
					if(playerIndex == -1 || neighbourDirection(currNodes[i])[j].playerIndex == -1 || neighbourDirection(currNodes[i])[j].playerIndex == playerIndex){
						nextNodes.Add(neighbourDirection(currNodes[i])[j]);
					}
					
				}
			}
			currNodes = nextNodes;
			remainingSteps -= 1;
		}
		finalNodes = currNodes;
		if(!canMoveToOwnPiece){
			finalNodes.RemoveAll(node => node.currPieces.Any(piece => piece.playerIndex == playerIndex));
			finalNodes.RemoveAll(node => !node.canBeSteppedOn);
		}

		return finalNodes;
	}

	private List<BoardNodeController> BreadthFirstSearchFromNode(BoardNodeController destination, Func<BoardNodeController, List<BoardNodeController>> neighbourDirection){

	}
	private List<BoardNodeController> BreadthFirstSearchFromNodeBackwards(BoardNodeController destination){
		return BreadthFirstSearchFromNode(destination, node => node.inNeighbours);
	}
	private List<BoardNodeController> BreadthFirstSearchFromNodeForward(BoardNodeController destination){
		return BreadthFirstSearchFromNode(destination, node => node.inNeighbours);
	}

	public int GetDistanceFromStartNode(BasePlayerController player){
		return BreadthFirstSearchFromNodeBackwards(GameController.Instance.boardController.GetStartNode(player)).Count;
	}*/

	public void ChainOnStepModifiers(PieceController piece, bool addKickVisualEffect){
		_modsQueuePiece = piece;
		List<BoardNodeModifier> mods = GetModifiers();
		for (int i = 0; i < mods.Count; i++)
		{
			mods[i].OnModifierApplied += ApplyNextOnStepModInQueue;
		}
		_onStepModQueue = new Queue<BoardNodeModifier>(mods);

		if(addKickVisualEffect){
			VisualEffectController effect = GameController.Instance.visualEffectPool.PlayVisualEffect("kick_piece_visual_effect", GlobalPosition);
			effect.OnEffectEnded += _OnKickEffectFinishedStartModifierChain;
		}
		else{
			ApplyNextOnStepModInQueue(null);
		}	
	}
	private void _OnKickEffectFinishedStartModifierChain(VisualEffectController effect){
		effect.OnEffectEnded -= _OnKickEffectFinishedStartModifierChain;
		ApplyNextOnStepModInQueue(null);
	}
	private void ApplyNextOnStepModInQueue(BoardNodeModifier triggeringMod){
		if (triggeringMod != null)
		{
			triggeringMod.OnModifierApplied -= ApplyNextOnStepModInQueue;
		}
		
		if(_onStepModQueue.Count > 0){
			BoardNodeModifier mod = _onStepModQueue.Dequeue();
			mod.ApplyOnStepModifier(_modsQueuePiece);
		}
		else{
			EmitSignal(SignalName.OnAllOnStepModifierApplied, this);
		}
	}
	public abstract void Highlight();
	public abstract void RemoveHighlight();
	public abstract void DoOnLeaveNodeAction(PieceController piece);
	/// <Summary>
	/// Actions to do immeadeatly on PieceController arrival
	/// </Summary>
	public abstract void DoOnStepNodeAction(PieceController piece);

}
