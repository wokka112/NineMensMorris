using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private PieceColour colour;
    private Space space;

    public PieceColour GetColour()
    {
        return colour;
    }

    public Space GetSpace()
    {
        return space;
    }

    public void SetSpace(Space space)
    {
        this.space = space;
    }

    public enum PieceColour
    {
        WHITE,
        BLACK
    }
}
