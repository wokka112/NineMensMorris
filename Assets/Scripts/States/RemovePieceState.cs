using UnityEngine;

public class RemovePieceState : IState  
{
    private const IState.State state = IState.State.Remove_Piece;

    private readonly StateMachine stateMachine;
    private readonly GameController gameController;
    private readonly GameInputManager gameInputManager;
    private readonly IState.State nextState;

    public RemovePieceState(StateMachine stateMachine, GameController gameController, GameInputManager gameInputManager, IState.State nextState)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
        this.gameInputManager = gameInputManager;
        this.nextState = nextState;
    }

    public IState.State GetState()
    {
        return state;
    }

    public void Process()
    {
        HighlightRemovablePieces();
        if (gameInputManager.IsClickClicked())
        {
            Piece piece = gameController.GetPieceClicked(gameInputManager.GetMousePoint());
            if (piece != null && piece.IsSelectable())
            {
                gameController.RemovePiece(piece);
                DeHighlightRemovablePieces();

                stateMachine.SetCurrentState(nextState);
            }
        }
    }

    private void HighlightRemovablePieces()
    {
        gameController.MakeOpponentsRemovablePiecesSelectable();
    }

    private void DeHighlightRemovablePieces()
    {
        gameController.MakeOpponentsPiecesUnselectable();
    }

    public bool IsFinalState()
    {
        return false;
    }
}
