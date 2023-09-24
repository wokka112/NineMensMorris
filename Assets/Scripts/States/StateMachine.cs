using UnityEngine;
using System.Collections.Generic;

public abstract class StateMachine 
{
    protected IState currentState;
    protected Dictionary<IState.State, IState> states;
    protected List<IStateListener> listeners;
    protected GameController gameController;

    public StateMachine(GameController gameController)
    {
        this.gameController = gameController;
        states = new Dictionary<IState.State, IState>();
        listeners = new List<IStateListener>();
    }

    public void AddListener(IStateListener listener)
    {
        listeners.Add(listener);
    }

    public List<IStateListener> getListeners()
    {
        return listeners;
    }

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

    public void SetCurrentState(IState.State state)
    {
        states.TryGetValue(state, out IState nextState);
        if (nextState == null)
        {
            Debug.LogError("No state exists for: " + state);
            SetCurrentState(IState.State.Error);
        } else
        {
            SetCurrentState(nextState);
        }
    }

    public void SetCurrentState(IState nextState)
    {
        NotifyListeners(nextState.GetState());
        currentState = nextState;
    }

    public bool IsOnFinalState()
    {
        return currentState.IsFinalState();
    }

    private void NotifyListeners(IState.State state)
    {
        listeners.ForEach(delegate (IStateListener listener)
        {
            listener.OnStateChange(state);
        });
    }
}
