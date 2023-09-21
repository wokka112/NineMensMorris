using UnityEngine;

//TODO should probably make this more robust. Can I pass the error to it and then do something with it?
public class ErrorState : IGameState 
{
    private const IGameState.GameState state = IGameState.GameState.Error;

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        //TODO should this be true?
        return true;
    }

    public void Process()
    {
        Debug.LogError("An error occurred! If it keeps happening, please report it.");
    }
}
