using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    private GridXZ<Piece> grid;
    private int x;
    private int y;
    private PieceColour colour;

    public Piece(GridXZ<Piece> grid, int x, int y, PieceColour colour)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.colour = colour;
    }

    override public string ToString()
    {
        return x + "," + y + "\n" + colour.ToString();
    }

    public enum PieceColour
    {
        WHITE,
        BLACK
    }
}
