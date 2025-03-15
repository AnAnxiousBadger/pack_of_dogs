using Godot;
using System;
using System.Collections.Generic;

public class AIMove
{
    private PieceController _piece;
    public PieceController Piece
    {
        get { return _piece; }
    }
    private BoardNodeController _destinationNode;
    public BoardNodeController DestinationNode
    {
        get { return _destinationNode; }
    }
    private int _rollToGetToNode;
    private float _score;
    public float Score
    {
        get { return _score; }
    }

    public AIMove(PieceController piece, BoardNodeController destinationNode, int roll)
    {
        this._piece = piece;
        this._destinationNode = destinationNode;
        this._rollToGetToNode = roll;
        this._score = CalculateScore();
    }

    private float CalculateScore()
    {
        float score = 0;

        // POSITIVE SCORES
        score += _rollToGetToNode; // Score for moving forward

        List<PieceController> enemyPiecesOnDestinationNode = _destinationNode.GetEnemyPieces(_piece.player);
        if (enemyPiecesOnDestinationNode.Count > 0)
        {
            foreach (PieceController enemyPiece in enemyPiecesOnDestinationNode)
            {
                score += GlobalHelper.Instance.GameController.boardController.GetDistanceFromStartNode(_destinationNode, enemyPiece.player); // Score for hitting an enemy
            }

        }
        // Score for modifiers
        if (_destinationNode.HasModifier("double_turn_modifier"))
        {
            score += _piece.player.rollSettings.GetAverageRoll();
        }
        if (_destinationNode.HasModifier("protected_node_modifier"))
        {
            score += 2f;
        }

        score += CalculateGettingHitScore(_piece.currNode) - CalculateGettingHitScore(_destinationNode);

        return score;
    }

    private float CalculateGettingHitScore(BoardNodeController stayingAtNode)
    {
        if (stayingAtNode.HasModifier("protected_node_modifier") || stayingAtNode is EndNodeController)
        {
            return 0f;
        }

        float gettingHitScore = 0f;

        int distanceFromStartNode = GlobalHelper.Instance.GameController.boardController.GetDistanceFromStartNode(stayingAtNode, _piece.player);
        // For enemy players
        foreach (BasePlayerController player in GlobalHelper.Instance.GameController.players)
        {
            if (player != _piece.player)
            {
                // Get possible rolls for enemies
                List<int> rollables = player.rollSettings.GetRollables();
                Dictionary<int, float> rollChanceDictWitchChances = player.rollSettings.GetRollChanceDictWithChances();

                Queue<BoardNodeController> nodesToCheckFrom = new();
                nodesToCheckFrom.Enqueue(stayingAtNode);
                Queue<float> probabilityFactors = new();
                probabilityFactors.Enqueue(1f);

                while (nodesToCheckFrom.Count > 0)
                { // Recursion for double rolls
                    BoardNodeController currCheckedNode = nodesToCheckFrom.Dequeue();
                    float probabilityFactor = probabilityFactors.Dequeue();
                    foreach (int roll in rollables)
                    {
                        // Move backwards the roll amount
                        List<BoardNodeController> backwardsNodesForRoll = GlobalHelper.Instance.GameController.boardController.MoveBackwardsAlongNodesFromNode(currCheckedNode, roll, player.playerIndex, true);
                        foreach (BoardNodeController node in backwardsNodesForRoll)
                        {
                            // if there is enemy → add roll chance * possible backwards move to negative score
                            if (node.GetEnemyPieces(_piece.player).Count > 0)
                            {
                                for (int i = 0; i < node.GetEnemyPieces(_piece.player).Count; i++)
                                {
                                    //GD.Print(node.Name + " - " + (float)rollChanceDictWitchChances[roll] * probabilityFactor);
                                    gettingHitScore += (float)distanceFromStartNode * rollChanceDictWitchChances[roll] * probabilityFactor;
                                }
                            }
                            else if (node.HasModifier("double_turn_modifier"))
                            {
                                // if it is double turn without enemy check for enemies recursively from double turn modifier containing nodes
                                // multiply probabilities
                                nodesToCheckFrom.Enqueue(node);
                                probabilityFactors.Enqueue(rollChanceDictWitchChances[roll] * probabilityFactor);
                            }
                        }

                    }

                }
            }
        }

        return gettingHitScore;
    }
}