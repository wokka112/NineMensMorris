using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : IGameState
{
    private GameStateMachine stateMachine;
    private BoardState boardState;
    private IGameState.STATE state = IGameState.STATE.INIT;
    

    public InitState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }


    public void Process()
    {
        //Debug.Log("Processing init state");
        boardState.MakeAllEmptySpacesSelectable();
        stateMachine.SetCurrentState(IGameState.STATE.SETUP);
    }

    public IGameState.STATE GetState()
    {
        return state;
    }
}
