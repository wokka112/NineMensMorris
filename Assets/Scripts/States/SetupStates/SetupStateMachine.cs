public class SetupStateMachine : StateMachine
{
    private readonly GameController gameController;
    private readonly GameInputManager gameInputManager;

    public SetupStateMachine(GameController gameController, GameInputManager gameInputManager) 
    {
        this.gameController = gameController;
        this.gameInputManager = gameInputManager;
    }

    public override void Init()
    {
        states.Clear();
        SetupStates();
        SetCurrentState(IState.State.Setup_Highlight_Empty_Spaces);
    }

    private void SetupStates()
    {
        IState highlightEmptySpacesState = new HighlightEmptySpacesState(this, gameController);
        states.Add(highlightEmptySpacesState.GetState(), highlightEmptySpacesState); 

        IState placePieceState = new PlacePieceState(this, gameController, gameInputManager);
        states.Add(placePieceState.GetState(), placePieceState); 

        IState checkSetupEndState = new CheckSetupEndState(this, gameController);
        states.Add(checkSetupEndState.GetState(), checkSetupEndState);

        IState removePieceState = new RemovePieceState(this, gameController, gameInputManager, checkSetupEndState.GetState());
        states.Add(removePieceState.GetState(), removePieceState);

        IState postState = new PostState(this, gameController);
        states.Add(postState.GetState(), postState);

        IState finalState = new FinalState();
        states.Add(finalState.GetState(), finalState);
    }
}
