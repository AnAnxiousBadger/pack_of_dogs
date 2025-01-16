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
	public virtual Vector3 TopPos{
		get { return GlobalPosition + new Vector3(0f, 0.6f, 0f);}
	}
	private Queue<BoardNodeModifier> _onStepModQueue = new();
	private PieceController _modsQueuePiece;
	// SIGNALS
	//[Signal] public delegate void OnStepEventHandler(PieceController piece);
	[Signal] public delegate void OnLeaveEventHandler(PieceController piece);
	[Signal] public delegate void OnAllOnStepModifierAppliedEventHandler(BoardNodeController node);
	
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
	public PieceController GetEnemyPiece(BasePlayerController ownPlayer){
		PieceController enemy = null;
		for (int i = 0; i < currPieces.Count; i++)
		{
			if(currPieces[i].player != ownPlayer){
				enemy = currPieces[i];
			}
		}
		return enemy;
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
			DoOnStepNodeAction(_modsQueuePiece);
			EmitSignal(SignalName.OnAllOnStepModifierApplied, this);
		}
	}
	public abstract void Highlight();
	public abstract void RemoveHighlight();
	public abstract void DoOnLeaveNodeAction(PieceController piece);
	public abstract void DoOnStepNodeAction(PieceController piece);

}
