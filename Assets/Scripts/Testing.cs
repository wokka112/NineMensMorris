using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridXZ<Piece> grid;

    void Start()
    {
        grid = new GridXZ<Piece>(7, 7, 5f, Vector3.zero, CreateGridObject, true);
    }

    public Piece CreateGridObject(GridXZ<Piece> grid, int x, int y)
    {
        int colorNum = (x + y) % 2;

        return new Piece(grid, x, y, colorNum == 0 ? Piece.PieceColour.WHITE : Piece.PieceColour.BLACK);
    }
}
