public class FinalState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Final;

    private readonly GameController gameController;

    public FinalState(GameController gameController)
    {
        this.gameController = gameController;
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
        gameController.MakeAllSpacesUnselectable();
    }
}
