using UnityEngine;

public class TurnMovePieceState : IState
{
    private const IState.State state = IState.State.Turn_Move_Piece;

    private GameStateMachine stateMachine;
    private GameController gameController;
    private GameInputManager gameInputManager;

    public TurnMovePieceState(GameStateMachine stateMachine, GameController gameController, GameInputManager gameInputManager)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
        this.gameInputManager = gameInputManager;
    }

    public void Process()
    {
        if (gameInputManager.IsClickClicked())
        {
            Space space = gameController.GetSpaceClicked(gameInputManager.GetMousePoint());
            if (space != null && space.IsSelectable())
            {
                Piece piece = gameController.GetSelectedPiece();
                gameController.MovePiece(piece, space);
                gameController.MakeAllSpacesUnselectable();
                stateMachine.SetCurrentState(IState.State.Turn_Decision_Making);
            }
        } else if (gameInputManager.IsRightClickClicked())
        {
            gameController.DeselectSelectedPiece();
            gameController.MakeAllSpacesUnselectable();
            stateMachine.SetCurrentState(IState.State.Turn_Start);
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
