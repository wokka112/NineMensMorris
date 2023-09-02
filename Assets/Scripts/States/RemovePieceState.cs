using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePieceState : IState, ISetupState 
{
    private const IGameState.GameState state = IGameState.GameState.Remove_Piece;
    private const ISetupState.SetupState setupState = ISetupState.SetupState.Remove_Piece;

    private StateMachine stateMachine;
    private BoardState boardState;
    private IState nextState;

    public RemovePieceState(StateMachine stateMachine, BoardState boardState, IState nextState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
        this.nextState = nextState;
    }

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public ISetupState.SetupState GetSetupState()
    {
        return setupState;
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

                stateMachine.SetCurrentState(nextState);
            }
        }
    }

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

    public bool IsFinalState()
    {
        return false;
    }
}
