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
    private Piece selectedPiece = null;

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

    public void SetSelectedPiece(Piece piece)
    {
        this.selectedPiece = piece;
    }

    public void DeselectSelectedPiece()
    {
        this.selectedPiece = null;
    }

    public Piece GetSelectedPiece()
    {
        return selectedPiece;
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

    public void MakeBlackPiecesUnselectable()
    {
        MakePiecesUnselectable(blackPieces);
    }

    public void MakeCurrentPlayersPiecesUnselectable()
    {
        if (currentPlayer == Player.WHITE)
        {
            MakeWhitePiecesUnselectable();
        } else
        {
            MakeBlackPiecesUnselectable();
        }
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
        Space space = null;
        RaycastHit hitData;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hitData, 1000, spaceLayer))
        {
            space = hitData.transform.GetComponent<Space>();
        }

        return space;
    }

    public Piece GetPieceClicked(Vector3 mousePosition)
    {
        Piece piece = null;
        RaycastHit hitData;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hitData, 1000, pieceLayer))
        {
            Debug.Log("Ray hit something: " + hitData);
            piece = hitData.transform.GetComponent<Piece>();
        }

        return piece;
    }

    public void MakeMovableSpacesSelectable(Piece piece)
    {
        Space space = piece.GetSpace();
        if (space == null)
        {
            // TODO throw an exception maybe? Can catch, log and then switch to different state?
            Debug.LogError("This piece has no space!");
            return;
        }

        space.GetNeighbours().ForEach(delegate(Space space)
        {
            space.SetSelectable();
        });
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
