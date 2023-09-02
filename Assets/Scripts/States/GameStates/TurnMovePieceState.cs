using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMovePieceState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Move_Piece;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnMovePieceState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = boardState.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = boardState.GetSelectedPiece();
                piece.Move(space);
                boardState.MakeAllSpacesUnselectable();
                stateMachine.SetCurrentState(IGameState.GameState.Turn_Decision_Making);
            } else
            {
                Debug.Log("Please click a selectable space");
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            boardState.DeselectSelectedPiece();
            boardState.MakeAllSpacesUnselectable();
            stateMachine.SetCurrentState(IGameState.GameState.Turn_Setup);
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
