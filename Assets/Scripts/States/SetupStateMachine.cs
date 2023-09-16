using System.Collections.Generic;
using UnityEngine;

public class SetupStateMachine : StateMachine
{
    private readonly GameController gameController;
    private Dictionary<ISetupState.SetupState, IState> setupStates;

    public SetupStateMachine(GameController gameController)
    {
        this.gameController = gameController;
        SetupStates();
        SetCurrentState(ISetupState.SetupState.Highlight_Empty_Spaces);
    }

    public void SetCurrentState(ISetupState.SetupState state)
    {
        setupStates.TryGetValue(state, out IState nextState);
        if (nextState == null)
        {
            Debug.LogError("No state exists for: " + state);
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

        ISetupState highlightEmptySpacesState = new HighlightEmptySpacesState(this, gameController);
        setupStates.Add(highlightEmptySpacesState.GetSetupState(), highlightEmptySpacesState); 

        ISetupState placePieceState = new PlacePieceState(this, gameController);
        setupStates.Add(placePieceState.GetSetupState(), placePieceState); 

        ISetupState removePieceState = new RemovePieceState(this, gameController, highlightEmptySpacesState);
        setupStates.Add(removePieceState.GetSetupState(), removePieceState);

        ISetupState checkSetupEndState = new CheckSetupEndState(this, gameController);
        setupStates.Add(checkSetupEndState.GetSetupState(), checkSetupEndState);

        ISetupState finalState = new FinalState(gameController);
        setupStates.Add(finalState.GetSetupState(), finalState); 
    }
}
