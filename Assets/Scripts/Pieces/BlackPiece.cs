using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPiece : Piece 
{
    public Piece.PieceColour GetColour()
    {
        return Piece.PieceColour.BLACK;
    }
}
