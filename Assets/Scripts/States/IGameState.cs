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
        Turn_Setup,
        Turn_Pick_Piece,
        Turn_Move_Piece,
        Turn_Decision_Making,
        Turn_End,
        Remove_Piece,
        Game_End,
        Error
    }
}
