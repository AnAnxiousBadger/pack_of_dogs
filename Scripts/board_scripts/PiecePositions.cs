using System;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class PiecePositions : Resource{
    [Export] private PiecePosition[] positions;

    public PiecePositions(){
        positions = Array.Empty<PiecePosition>();
    }
    public void AddPosToPositions(PiecePosition pos){
        List<PiecePosition> positions = new();
        for (int i = 0; i < this.positions.Length; i++)
        {
            positions.Add(this.positions[i]);
        }
        positions.Add(pos);
        this.positions = positions.ToArray();
    }

    public Vector3 TakeFreePosition(PieceController piece){
		for (int i = 0; i < positions.Length; i++)
		{
			PiecePosition piecePos = positions[i];
			if(piecePos.playerIndex == piece.playerIndex && !piecePos.isOccupied){
				piecePos.isOccupied = true;
				piecePos.piece = piece;
				return piecePos.pos;
			}
		}
		return Vector3.Zero;
	}

    public void FreeUpPosition(PieceController piece){
		for (int i = 0; i < positions.Length; i++)
		{
			PiecePosition piecePos = positions[i];
			if(piecePos.piece == piece){
				piecePos.isOccupied = false;
				piecePos.piece = null;
				break;
			}
		}
	}

}