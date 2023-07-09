using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public void Process();

    public GameState GetState();

    public enum GameState
    {
        Init,
        Board_Setup,
        Turn_Start,
        Turn_Pick_Piece,
        Turn_Move_Piece,
        Turn_Remove_Piece,
        Check_End_Condition,
        End
    }

    // PLAY_TURN_START - highlights selectable pieces 
    // PLAY_TURN_PICK_PIECE - gets player to pick piece, then highlights selectable spaces
    // PLAY_TURN_MOVE_PIECE - gets player to pick available space, moves piece, checks if a mill is made
    // PLAY_TURN_REMOVE_PIECE - highlights pieces player can remove
    // PLAY_CHECK_END_CONDITIONS - if only 2 pieces are left to any 1 player, game ends, otherwise, return to start.
}
