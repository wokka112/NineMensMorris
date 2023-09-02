using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupStateMachine : StateMachine
{
    private readonly BoardState boardState;
    private Dictionary<ISetupState.SetupState, IState> setupStates;

    public SetupStateMachine(BoardState boardState)
    {
        this.boardState = boardState;
        SetupStates();
        SetCurrentState(ISetupState.SetupState.Highlight_Empty_Spaces);
    }

    public void SetCurrentState(ISetupState.SetupState state)
    {
        Debug.Log("Setting current state to: " + state);
        setupStates.TryGetValue(state, out IState nextState);
        if (nextState == null)
        {
            throw new UnityException("No state exists for setup state: " + state);
        }
        else
        {
            SetCurrentState(nextState);
        }
    }

    public bool IsOnFinalState()
    {
        return currentState.IsFinalState();
    }

    private void SetupStates()
    {
        setupStates = new Dictionary<ISetupState.SetupState, IState>();

        ISetupState highlightEmptySpacesState = new HighlightEmptySpacesState(this, boardState);
        setupStates.Add(highlightEmptySpacesState.GetSetupState(), highlightEmptySpacesState); 

        ISetupState placePieceState = new PlacePieceState(this, boardState);
        setupStates.Add(placePieceState.GetSetupState(), placePieceState); 

        ISetupState removePieceState = new RemovePieceState(this, boardState, highlightEmptySpacesState);
        setupStates.Add(removePieceState.GetSetupState(), removePieceState);

        ISetupState checkSetupEndState = new CheckSetupEndState(this, boardState);
        setupStates.Add(checkSetupEndState.GetSetupState(), checkSetupEndState);

        ISetupState finalState = new FinalState(boardState);
        setupStates.Add(finalState.GetSetupState(), finalState); 
    }
}
