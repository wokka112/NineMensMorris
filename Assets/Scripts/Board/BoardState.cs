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

    public void RemovePiece(Piece piece)
    {
        piece.GetSpace().RemovePiece();
        if (piece.GetColour() == Piece.Colour.WHITE)
        {
            whitePieces.Remove(piece);
        } else
        {
            blackPieces.Remove(piece);
        }

        GameObject.Destroy(piece.gameObject);
    }

    public bool IsGameOver()
    {
        return whitePieces.Count <= 2 || blackPieces.Count <= 2;
    }

    public Player GetWinner()
    {
        if (blackPieces.Count <= 2)
        {
            return Player.WHITE;
        }
        else if (whitePieces.Count <= 2)
        {
            return Player.BLACK;
        } else
        {
            //TODO change to dif exception. Make a homemade exception. Or just log or something
            throw new System.Exception("Nobody won yet!");
        }
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

    public void MakeWhitePiecesThatCanMoveSelectable()
    {
        MakePiecesThatCanMoveSelectable(whitePieces);
    }

    public void MakeBlackPiecesThatCanMoveSelectable()
    {
        MakePiecesThatCanMoveSelectable(blackPieces);
    }

    private void MakePiecesThatCanMoveSelectable(List<Piece> pieces)
    {
        foreach(Piece piece in pieces)
        {
            if (piece.GetSpace().HasEmptyNeighbour())
            {
                piece.SetSelectable();
            }
        }
    }

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

    public void MakeWhiteRemovablePiecesSelectable()
    {
        MakeRemovablePiecesSelectable(whitePieces);
    }

    public void MakeBlackRemovablePiecesSelectable()
    {
        MakeRemovablePiecesSelectable(blackPieces);
    }

    private void MakeRemovablePiecesSelectable(List<Piece> pieces)
    {
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
            MakePiecesSelectable(pieces);
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
