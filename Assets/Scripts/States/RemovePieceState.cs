using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePieceState : IGameState, ISetupState 
{
    private const IGameState.GameState state = IGameState.GameState.Remove_Piece;
    private const ISetupState.SetupState setupState = ISetupState.SetupState.Remove_Piece;

    private StateMachine stateMachine;
    private GameController gameController;
    private IState nextState;

    public RemovePieceState(StateMachine stateMachine, GameController gameController, IState nextState)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
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
            Piece piece = gameController.GetPieceClicked(Input.mousePosition);
            if (piece != null && piece.IsSelectable())
            {
                gameController.RemovePiece(piece);
                DeHighlightRemovablePieces();

                stateMachine.SetCurrentState(nextState);
            }
        }
    }

    private void HighlightRemovablePieces()
    {
        gameController.MakeOpponentsRemovablePiecesSelectable();
    }

    private void DeHighlightRemovablePieces()
    {
        gameController.MakeOpponentsPiecesUnselectable();
    }

    public bool IsFinalState()
    {
        return false;
    }
}
