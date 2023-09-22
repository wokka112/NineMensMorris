using UnityEngine;

public class PlacePieceState : IState
{
    private const IState.State state = IState.State.Setup_Place_Piece;

    private readonly GameController gameController;
    private readonly StateMachine stateMachine;

    public PlacePieceState(StateMachine stateMachine, GameController gameController)
    {
        this.gameController = gameController;
        this.stateMachine = stateMachine;
    }

    public IState.State GetState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = gameController.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = gameController.PlacePiece(space);
                if (piece == null)
                {
                    // TODO replace with exception? But that'd break the game flow, whereas we can just ignore it and continue trying this way
                    Debug.LogError("Something went wrong while creating the new piece!");
                }
                else
                {
                    space.SetUnselectable();
                    if (piece.IsPartOfAMill())
                    {
                        gameController.MakeAllSpacesUnselectable();
                        stateMachine.SetCurrentState(IState.State.Remove_Piece);
                    } else
                    {
                        stateMachine.SetCurrentState(IState.State.Setup_Check_Setup_End);
                    }
                }
            }
        }
    }
}
