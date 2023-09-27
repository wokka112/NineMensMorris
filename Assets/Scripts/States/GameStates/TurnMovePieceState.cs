using UnityEngine;

public class TurnMovePieceState : IState
{
    private const IState.State state = IState.State.Turn_Move_Piece;

    private GameStateMachine stateMachine;
    private GameController gameController;

    public TurnMovePieceState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = gameController.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = gameController.GetSelectedPiece();
                piece.Move(space);
                gameController.MakeAllSpacesUnselectable();
                stateMachine.SetCurrentState(IState.State.Turn_Decision_Making);
            }
        } else if (Input.GetMouseButtonDown(1))
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
