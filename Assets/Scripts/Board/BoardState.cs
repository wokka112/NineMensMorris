using System.Collections.Generic;
using UnityEngine;

public class BoardState
{
    private GameObject blackPiecePrefab;
    private GameObject whitePiecePrefab;

    private Space[] spaces;
    private List<Piece> whitePieces;
    private List<Piece> blackPieces;

    public BoardState(Space[] spaces, GameObject blackPiecePrefab, GameObject whitePiecePrefab)
    {
        this.spaces = spaces;
        this.blackPiecePrefab = blackPiecePrefab;
        this.whitePiecePrefab = whitePiecePrefab;
        whitePieces = new List<Piece>();
        blackPieces = new List<Piece>();
    }

    public Piece PlacePiece(Space space, Colour colour)
    {
        if (!space.IsEmpty())
        {
            return null;
        }

        Piece piece = CreatePiece(space.GetPosition(), colour);
        space.SetPiece(piece);
        piece.SetSpace(space);

        return piece;
    }

    private Piece CreatePiece(Vector3 position, Colour colour)
    {
        Piece piece;
        if (colour == Colour.BLACK)
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

    public void RemovePiece(Piece piece)
    {
        piece.GetSpace().RemovePiece();
        if (piece.GetColour() == Colour.WHITE)
        {
            whitePieces.Remove(piece);
        }
        else
        {
            blackPieces.Remove(piece);
        }

        GameObject.Destroy(piece.gameObject);
    }

    public void MakeAllEmptySpacesSelectable()
    {
        foreach (Space space in spaces)
        {
            if (space.IsEmpty())
            {
                space.SetSelectable();
            }
        }
    }

    public void MakeAllSpacesUnselectable()
    {
        foreach (Space space in spaces)
        {
            space.SetUnselectable();
        }
    }

    public void MakePiecesSelectable(Colour colour)
    {
        List<Piece> pieces = getPieces(colour);

        foreach (Piece piece in pieces)
        {
            piece.SetSelectable();
        }
    }

    public void MakePiecesUnselectable(Colour colour)
    {
        List<Piece> pieces = getPieces(colour);

        foreach (Piece piece in pieces)
        {
            piece.SetUnselectable();
        }
    }

    public bool IsThereAMovablePiece(Colour colour)
    {
        List<Piece> pieces = colour == Colour.WHITE ? whitePieces : blackPieces;

        foreach (Piece piece in pieces)
        {
            if (piece.GetSpace().HasEmptyNeighbour())
            {
                return true;
            }
        }

        return false;
    }

    public void MakePiecesThatCanMoveSelectable(Colour colour)
    {
        List<Piece> pieces = getPieces(colour);

        foreach (Piece piece in pieces)
        {
            if (piece.GetSpace().HasEmptyNeighbour())
            {
                piece.SetSelectable();
            }
        }
    }

    public void MakeSpacesPieceCanMoveToSelectable(Piece piece)
    {
        Space space = piece.GetSpace();
        if (space == null)
        {
            // Replace with an extended, custom exception.
            Debug.LogError("Pieces[" + piece + "] has no space associated with it!");
            throw new UnityException("Piece[" + piece + "] has no space associated with it!");
        }

        space.GetNeighbours().ForEach(delegate (Space space)
        {
            space.SetSelectable();
        });
    }


    public void MakeRemovablePiecesSelectable(Colour colour)
    {
        List<Piece> pieces = getPieces(colour);

        bool allInMills = true;

        foreach (Piece piece in pieces)
        {
            if (!piece.IsPartOfAMill())
            {
                piece.SetSelectable();
                allInMills = false;
            }
        }

        if (allInMills)
        {
            MakePiecesSelectable(colour);
        }
    }

    public int getPiecesLeft(Colour colour)
    {
        return getPieces(colour).Count;
    }

    private List<Piece> getPieces(Colour colour)
    {
        return colour == Colour.WHITE ? whitePieces : blackPieces;
    }
}
