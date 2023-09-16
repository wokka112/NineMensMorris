using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSetupState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Post_Setup;

    private readonly GameController gameController;
    private readonly SetupStateMachine stateMachine;

    public PostSetupState(SetupStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
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
        gameController.MakeAllSpacesUnselectable();
        stateMachine.SetCurrentState(ISetupState.SetupState.Final);
    }
}
