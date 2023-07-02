using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Piece
{
    PieceColour GetColour();

    public enum PieceColour
    {
        WHITE,
        BLACK
    }
}
