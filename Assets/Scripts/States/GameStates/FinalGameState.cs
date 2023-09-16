public class FinalGameState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Final;

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return true;
    }

    public void Process()
    {
    }
}
