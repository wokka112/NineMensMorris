using UnityEngine;
public class BoardSetupState : MonoBehaviour, IState
{
    private const IState.State state = IState.State.Board_Setup;

    private readonly GameStateMachine gameStateMachine;
    private readonly SetupStateMachine setupStateMachine;
    private readonly GameController gameController;

    public BoardSetupState(GameStateMachine gameStateMachine, GameController gameController)
    {
        this.gameStateMachine = gameStateMachine;
        this.gameController = gameController;
        setupStateMachine = gameStateMachine.GetSetupStateMachine();
    }

    public void Process()
    {
        if (setupStateMachine.IsOnFinalState())
        {
            if (gameController.IsGameOver())
            {
                gameStateMachine.SetCurrentState(IState.State.Game_End);
            }
            else
            {
                gameStateMachine.SetCurrentState(IState.State.Game_Start);
            }
        }
        else
        {
            setupStateMachine.Process();
        }
    }

    public IState.State GetState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }
}
