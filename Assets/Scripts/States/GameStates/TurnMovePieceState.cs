using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMovePieceState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Move_Piece;

    private GameStateMachine stateMachine;
    private GameController gameController;

    public TurnMovePieceState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = gameController.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = gameController.GetSelectedPiece();
                piece.Move(space);
                gameController.MakeAllSpacesUnselectable();
                stateMachine.SetCurrentState(IGameState.GameState.Turn_Decision_Making);
            } else
            {
                Debug.Log("Please click a selectable space");
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            gameController.DeselectSelectedPiece();
            gameController.MakeAllSpacesUnselectable();
            stateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
        }
    }

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }
}
