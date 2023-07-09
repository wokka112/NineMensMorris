using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState
{
    private GameObject blackPiecePrefab;
    private GameObject whitePiecePrefab;

    private Space[] allSpaces;
    private List<Piece> whitePieces;
    private List<Piece> blackPieces;
    private LayerMask spaceLayer;
    private LayerMask pieceLayer;
    private Player currentPlayer;

    public BoardState(Space[] allSpaces, GameObject blackPiecePrefab, GameObject whitePiecePrefab, LayerMask spaceLayer, LayerMask pieceLayer)
    {
        this.allSpaces = allSpaces;
        this.blackPiecePrefab = blackPiecePrefab;
        this.whitePiecePrefab = whitePiecePrefab;
        this.spaceLayer = spaceLayer;
        this.pieceLayer = pieceLayer;
        this.whitePieces = new List<Piece>();
        this.blackPieces = new List<Piece>();
        this.currentPlayer = Player.WHITE;
    }

    public Piece AddPieceToBoard(Space space)
    {
        if (!space.IsEmpty())
        {
            return null;
        }

        Piece piece = CreatePiece(space.GetPosition());
        space.SetPiece(piece);
        piece.SetSpace(space);

        return piece;
    }

    private Piece CreatePiece(Vector3 position)
    {
        Piece piece;
        if (currentPlayer == Player.BLACK)
        {
            piece = GameObject.Instantiate(blackPiecePrefab, position, Quaternion.identity).GetComponent<Piece>();
            blackPieces.Add(piece);
        }
        else
        {
            piece = GameObject.Instantiate(whitePiecePrefab, position, Quaternion.identity).GetComponent<Piece>();
            whitePieces.Add(piece);
        }

        return piece;
    }

    public void MakeAllEmptySpacesSelectable()
    {
        foreach (Space space in allSpaces)
        {
            if (space.IsEmpty())
            {
                space.SetSelectable();
            }
        }
    }

    public void MakeAllSpacesUnselectable()
    {
        foreach (Space space in allSpaces)
        {
            space.SetUnselectable();
        }
    }

    public void MakeWhitePiecesSelectable()
    {
        MakePiecesSelectable(whitePieces);
    }

    public void MakeBlackPiecesSelectable()
    {
        MakePiecesSelectable(blackPieces);
    }

    public void MakeWhitePiecesUnselectable()
    {
        MakePiecesUnselectable(whitePieces);
    }

    public void MakeBlackPiecesunSelectable()
    {
        MakePiecesUnselectable(blackPieces);
    }

    //TODO: Is it worth only making pieces with at least 1 empty space as a neighbour selectable?
    private void MakePiecesSelectable(List<Piece> pieces)
    {
        foreach (Piece piece in pieces)
        {
            piece.SetSelectable();
        }
    }

    private void MakePiecesUnselectable(List<Piece> pieces)
    {
        foreach(Piece piece in pieces)
        {
            piece.SetUnselectable();
        }
    }

    public Space GetSpaceClicked(Vector3 mousePosition)
    {
        Debug.Log("Getting space clicked");
        Space space = null;
        RaycastHit hitData;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hitData, 1000, spaceLayer))
        {
            Debug.Log("Hit object: " + hitData.transform.gameObject.name);
            space = hitData.transform.GetComponent<Space>();
            Debug.Log("Returning space: " + space.ToString());
        }
        else
        {
            Debug.Log("Didn't hit object!");
        }

        return space;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void SwitchPlayer()
    {
        if (currentPlayer == Player.WHITE)
        {
            currentPlayer = Player.BLACK;
        } else
        {
            currentPlayer = Player.WHITE;
        }
    }
}
