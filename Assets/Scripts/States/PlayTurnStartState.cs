using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO change this to PlayTurnSetupState? Then could add a TurnStartState which would show a UI message saying whose turn it is?
public class TurnStartState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Start;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnStartState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        // Highlight selectable pieces.
        if (boardState.GetCurrentPlayer() == Player.WHITE)
        {
            boardState.MakeWhitePiecesSelectable();
        } else
        {
            boardState.MakeBlackPiecesSelectable();
        }

        stateMachine.SetCurrentState(IGameState.GameState.Turn_Pick_Piece);
        
        //On click
            // if piece is player's piece
                // highlight selectable spaces
                // On click
                    // If space is a selectable space
                        // Move piece to space
                        // If mill is made
                            // Remove piece
                        // Switch players
                    // otherwise
                        // warn player is wrong space
            // otherwise
                // warn player is wrong piece

        // Could do states for this?
        // PLAY_TURN_START - highlights selectable pieces 
        // PLAY_TURN_PICK_PIECE - gets player to pick piece, then highlights selectable spaces
        // PLAY_TURN_MOVE_PIECE - gets player to pick available space, moves piece, checks if a mill is made
        // PLAY_TURN_REMOVE_PIECE - highlights pieces player can remove
        // PLAY_CHECK_END_CONDITIONS - if only 2 pieces are left to any 1 player, game ends, otherwise, return to start.
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
