using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePiece : Piece
{
    public Piece.PieceColour GetColour()
    {
        return Piece.PieceColour.WHITE;
    }
}
