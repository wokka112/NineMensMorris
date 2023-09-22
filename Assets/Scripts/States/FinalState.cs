public class FinalState : IState
{
    private const IState.State state = IState.State.Final;

    public IState.State GetState()
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
