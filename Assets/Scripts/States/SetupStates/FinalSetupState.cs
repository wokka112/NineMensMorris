public class FinalSetupState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Final;
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
    }
}
