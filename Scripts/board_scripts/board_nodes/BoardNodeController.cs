using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class BoardNodeController : StaticBody3D
{
	// EXPORTS
	[ExportCategory("Common Exports")]
	[Export] public Node3D topGuide;

	[ExportCategory("Per Instance Exports")]
	[Export] public int playerIndex = -1; // -1 for shared nodes
	[Export] public BoardNodeController[] outNeighbours = Array.Empty<BoardNodeController>();
	// REFERENCES
	public List<BoardNodeController> inNeighbours = new();
	// OTHER
	public bool canBeSteppedOn = true;
	public List<PieceController> currPieces;
	private Queue<BoardNodeModifier> _onStepModQueue = new();
	private PieceController _modsQueuePiece;
	// SIGNALS
	[Signal] public delegate void OnLeaveEventHandler(PieceController piece); // SHOULD BE HANDLED LIKE ONSTEP
	[Signal] public delegate void OnAllOnStepModifierAppliedEventHandler(BoardNodeController node);
	
	public virtual void SetUpNode(){
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

	public void ChainOnStepModifiers(PieceController piece, bool addKickVisualEffect){
		_modsQueuePiece = piece;
		List<BoardNodeModifier> mods = GetModifiers();
		for (int i = 0; i < mods.Count; i++)
		{
			mods[i].OnModifierApplied += ApplyNextOnStepModInQueue;
		}
		_onStepModQueue = new Queue<BoardNodeModifier>(mods);

		if(addKickVisualEffect){
			VisualEffectController effect = GlobalHelper.Instance.GameController.visualEffectPool.PlayVisualEffect("kick_piece_visual_effect", GlobalPosition, GlobalRotation);
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
