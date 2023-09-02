using UnityEngine;

public class FinalState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Final;

    private readonly BoardState boardState;

    public FinalState(BoardState boardState)
    {
        this.boardState = boardState;
    }

    public ISetupState.SetupState GetSetupState()
    {
        return state;
    }

    public bool IsFinalState()
    {
       return true;
    }

    public void Process()
    {
        boardState.MakeAllSpacesUnselectable();
    }
}
