using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{
    protected IState currentState;

    public void Process()
    {
        try
        {
            currentState.Process();
        } catch (UnityException e)
        {
            Debug.LogException(e);
            SetCurrentState(new ErrorState());
        }
    }

    public void SetCurrentState(IState nextState)
    {
        this.currentState = nextState;
    }
}
