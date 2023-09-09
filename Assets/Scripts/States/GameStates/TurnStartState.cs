using UnityEngine;

public class TurnStartState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Start;

    private GameStateMachine stateMachine;
    private GameController gameController;

    public TurnStartState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        //TODO replace this with GUI stuff when we have one to say it's the player's turn.
        Debug.Log(gameController.GetCurrentPlayer() + "'s turn!");

        gameController.MakeCurrentPlayersMovablePiecesSelectable();
        stateMachine.SetCurrentState(IGameState.GameState.Turn_Pick_Piece);
    }

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }
}
