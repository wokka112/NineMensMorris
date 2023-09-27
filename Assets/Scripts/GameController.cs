using UnityEngine;

public class GameController : IStateListener
{
    private const int piecesToPlace = 9;

    private readonly UiHandler uiHandler;

    private LayerMask spaceLayer;
    private LayerMask pieceLayer;

    private BoardState boardState;
    private Colour currentPlayer;
    private Colour? winner;
    private Piece selectedPiece;
    private int blackPiecesPlaced;

    public GameController(Space[] allSpaces, GameObject blackPiecePrefab, GameObject whitePiecePrefab, LayerMask spaceLayer, LayerMask pieceLayer, UiHandler uiHandler)
    {
        this.spaceLayer = spaceLayer;
        this.pieceLayer = pieceLayer;
        this.uiHandler = uiHandler;

        boardState = new BoardState(allSpaces, blackPiecePrefab, whitePiecePrefab);
        currentPlayer = Colour.WHITE;
        winner = null;
        selectedPiece = null;
        blackPiecesPlaced = 0;
    }

    public Colour GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void SwitchPlayer()
    {
        if (currentPlayer == Colour.WHITE)
        {
            currentPlayer = Colour.BLACK;
        } else
        {
            currentPlayer = Colour.WHITE;
        }
    }

    public void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
    }

    public void DeselectSelectedPiece()
    {
        selectedPiece = null;
    }

    public Piece GetSelectedPiece()
    {
        return selectedPiece;
    }

    public bool HaveAllPiecesBeenPlaced()
    {
        return blackPiecesPlaced >= piecesToPlace;
    }

    private void IncrementBlackPiecesPlaced()
    {
        blackPiecesPlaced++;
    }

    public Piece PlacePiece(Space space)
    {
        Piece piece = boardState.PlacePiece(space, currentPlayer);

        if (piece != null && currentPlayer == Colour.BLACK)
        {
            IncrementBlackPiecesPlaced();
        }

        return piece;
    }

    public void RemovePiece(Piece piece)
    {
        boardState.RemovePiece(piece);
    }

    public bool IsAbleToMove(Colour colour)
    {
        return boardState.IsThereAMovablePiece(colour);
    }

    public bool IsGameOver()
    {
        return !IsAbleToMove(Colour.WHITE)
            || !IsAbleToMove(Colour.BLACK)
            || boardState.getPiecesLeft(Colour.WHITE) <= 2 
            || boardState.getPiecesLeft(Colour.BLACK) <= 2;
    }

    public Colour? GetWinner()
    {
        if (!IsAbleToMove(Colour.WHITE) || boardState.getPiecesLeft(Colour.WHITE) <= 2)
        {
            winner = Colour.BLACK;
        } else if (!IsAbleToMove(Colour.BLACK) || boardState.getPiecesLeft(Colour.BLACK) <= 2) 
        { 
            winner = Colour.WHITE;
        }

        return winner;
    }

    public void MakeAllEmptySpacesSelectable()
    {
        boardState.MakeAllEmptySpacesSelectable();
    }

    public void MakeAllSpacesUnselectable()
    {
        boardState.MakeAllSpacesUnselectable();
    }

    public void MakeCurrentPlayersMovablePiecesSelectable()
    {
        boardState.MakePiecesThatCanMoveSelectable(currentPlayer);
    }

    public void MakeCurrentPlayersPiecesUnselectable()
    {
        boardState.MakePiecesUnselectable(currentPlayer);
    }

    public void MakeOpponentsPiecesUnselectable()
    {
        boardState.MakePiecesUnselectable(currentPlayer == Colour.WHITE ? Colour.BLACK : Colour.WHITE);
    }

    public void MakeOpponentsRemovablePiecesSelectable()
    {
        boardState.MakeRemovablePiecesSelectable(currentPlayer == Colour.WHITE ? Colour.BLACK : Colour.WHITE);
    }

    public void MakeSpacesPieceCanMoveToSelectable(Piece piece)
    {
        boardState.MakeSpacesPieceCanMoveToSelectable(piece);
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
            piece = hitData.transform.GetComponent<Piece>();
        }

        return piece;
    }

    public void OnStateChange(IState.State state)
    {
        switch (state)
        {
            case (IState.State.Board_Setup):
                uiHandler.SetImportantText("BOARD SETUP", 1f, true);
                break;
            case (IState.State.Setup_Place_Piece):
                uiHandler.SetPromptText(currentPlayer.ToString() + "'s turn!", 0.5f, false);
                uiHandler.SetPromptText("Place piece");
                break;
            case (IState.State.Setup_Check_Setup_End):
                uiHandler.HidePromptText();
                break;
            case (IState.State.Game_Start):
                uiHandler.SetImportantText("GAME START", 1f, true);
                break;
            case (IState.State.Turn_Start):
                uiHandler.SetPromptText(currentPlayer.ToString() + "'s turn!", 0.5f, false);
                break;
            case (IState.State.Turn_Pick_Piece):
                uiHandler.SetPromptText("Select piece to move");
                break;
            case (IState.State.Turn_Move_Piece):
                uiHandler.SetPromptText("Select where to move");
                break;
            case (IState.State.Turn_End):
                uiHandler.HidePromptText();
                break;
            case (IState.State.Game_End):
                uiHandler.SetImportantText(GetWinner() + " won!", 1f, false);
                uiHandler.ClearPromptItems();
                uiHandler.HidePromptText();
                break;
            case (IState.State.Remove_Piece):
                uiHandler.SetPromptText("Select piece to remove");
                break;
            case (IState.State.Error):
                //TODO what to do here???
                // How do we deal with it?
                uiHandler.SetImportantText("An error occurred!", 2f, false);
                uiHandler.ClearPromptItems();
                uiHandler.HidePromptText();
                break;
            default:
                break;
        }
    }
}
