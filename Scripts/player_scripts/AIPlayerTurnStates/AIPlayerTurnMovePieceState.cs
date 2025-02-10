using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class AIPlayerTurnMovePieceState : PlayerTurnBaseState
{
    private readonly new AIPlayerController p;
    private readonly float _selectPieceWaitTime = 0.5f;
    private readonly float _selectNodeWaitTime = 0.5f;
    private AIMove _selectedMove;
    public override void EnterTurnState(){
        _selectedMove = GetBestMove();
        p.timer.WaitTime = _selectPieceWaitTime;
        p.timer.Timeout += _OnSelectPieceTimerTimeout;
        
        p.timer.Start();
    }

    public override void ExitTurnState(){}

    public override void ProcessTurnState(float delta){}

    private void _OnSelectPieceTimerTimeout(){
        p.timer.Timeout -= _OnSelectPieceTimerTimeout;
        _selectedMove.Piece.HighlightPiece(false);
        p.timer.WaitTime = _selectNodeWaitTime;
        p.timer.Timeout += _OnSelectNodeTimerTimeout;
        p.timer.Start();
    }
    private void _OnSelectNodeTimerTimeout(){
        p.timer.Timeout -= _OnSelectNodeTimerTimeout;

        BoardNodeController selectedNode = _selectedMove.DestinationNode;
        selectedNode.OnAllOnStepModifierApplied += _OnPieceMovedAndProcessed;
        GlobalClassesHolder.Instance.GameController.boardController.MovePiece(_selectedMove.Piece, selectedNode, false);
        p.EmitSignal(BasePlayerController.SignalName.PieceMoved, p.roll);
    }
    private void _OnPieceMovedAndProcessed(BoardNodeController destination){
        destination.OnAllOnStepModifierApplied -= _OnPieceMovedAndProcessed;
        p.SwitchToNextTurnState();
    }

    private AIMove GetBestMove(){
        int roll = p.roll;

        // Get all the possible nodes pieces can step on
        List<AIMove> possibleMoves = new();
        foreach (PieceController piece in p.pieces)
        {
            if(!piece.hasArrived){
                List<BoardNodeController> piecePossibleDestinations = GlobalClassesHolder.Instance.GameController.boardController.MoveForwardAlongNodesFromNode(piece.currNode, roll, piece.playerIndex, false);
                for (int i = 0; i < piecePossibleDestinations.Count; i++)
                {
                    possibleMoves.Add(new AIMove(piece, piecePossibleDestinations[i], roll));
                }
            }
        }
        // Get biggest score move or random from biggests → select piece
        List<AIMove> orderedMoves = possibleMoves.OrderByDescending(move => move.Score).ToList();
        /*foreach (AIMove move in orderedMoves)
        {
            GD.Print(move.Piece.Name + " - " + move.Score);
        }*/
        List<AIMove> largestScoreMoves = new() {orderedMoves[0]};
        for (int i = 1; i < orderedMoves.Count; i++)
        {
            if(orderedMoves[i].Score == orderedMoves[0].Score){
                largestScoreMoves.Add(orderedMoves[i]);
            }
        }
        AIMove selectedMove = largestScoreMoves[RandomGenerator.Instance.GetRandIntInRange(0, largestScoreMoves.Count - 1)];
        //GD.Print("Moved with: " + selectedMove.Piece.Name);
        //GD.Print("----------------------");
        return selectedMove;
    }

    public AIPlayerTurnMovePieceState(AIPlayerController p) : base(p){
        this.p = p;
    }
}