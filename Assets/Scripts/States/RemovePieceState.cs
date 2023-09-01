using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePieceState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Remove_Piece;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public RemovePieceState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public IGameState.GameState GetState()
    {
        return state;
    }

    public void Process()
    {
        HighlightRemovablePieces();
        if (Input.GetMouseButtonDown(0))
        {
            Piece piece = boardState.GetPieceClicked(Input.mousePosition);
            if (piece != null && piece.IsSelectable())
            {
                boardState.RemovePiece(piece);
                DeHighlightRemovablePieces();
                if (stateMachine.GetPreviousState().GetState() == IGameState.GameState.Board_Setup)
                {
                    boardState.SwitchPlayer();
                    stateMachine.SetCurrentState(IGameState.GameState.Init);
                } else
                {
                    stateMachine.SetCurrentState(IGameState.GameState.Turn_End);
                }
            }
        }
    }

    //TODO move into a state on it's own so it only happens once?
    private void HighlightRemovablePieces()
    {
        if (boardState.GetCurrentPlayer() == Player.WHITE)
        {
            boardState.MakeBlackRemovablePiecesSelectable();
        } else
        {
            boardState.MakeWhiteRemovablePiecesSelectable();
        }
    }

    private void DeHighlightRemovablePieces()
    {
        if (boardState.GetCurrentPlayer() == Player.WHITE)
        {
            boardState.MakeBlackPiecesUnselectable();
        } else
        {
            boardState.MakeWhitePiecesUnselectable();
        }
    }
}
