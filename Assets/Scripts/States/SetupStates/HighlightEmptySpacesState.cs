using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightEmptySpacesState : ISetupState
{
    private static ISetupState.SetupState state = ISetupState.SetupState.Highlight_Empty_Spaces;

    private SetupStateMachine stateMachine;
    private BoardState boardState;

    public HighlightEmptySpacesState(SetupStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public ISetupState.SetupState GetSetupState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        boardState.MakeAllEmptySpacesSelectable();
        stateMachine.SetCurrentState(ISetupState.SetupState.Place_Piece);
    }
}
