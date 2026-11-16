using UnityEngine;

public class TurnPickPieceState : IState
{
    private const IState.State state = IState.State.Turn_Pick_Piece;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;
    private readonly GameInputManager gameInputManager;

    public TurnPickPieceState(GameStateMachine stateMachine, GameController gameController, GameInputManager gameInputManager)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
        this.gameInputManager = gameInputManager;
    }

    public void Process()
    {
        if (gameInputManager.IsClickClicked())
        {
            Piece piece = gameController.GetPieceClicked(gameInputManager.GetMousePoint());
            if (piece != null && piece.IsSelectable())
            {
                if(!piece.CanMove())
                {
                    return;
                }

                gameController.PickUpPiece(piece);
                gameController.MakeCurrentPlayersPiecesUnselectable();
                gameController.MakeSpacesPieceCanMoveToSelectable(piece);

                stateMachine.SetCurrentState(IState.State.Turn_Move_Piece);
            }
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
