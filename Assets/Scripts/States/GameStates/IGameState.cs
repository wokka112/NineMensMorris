public interface IGameState : IState
{
    public IGameState.GameState GetGameState();

    public enum GameState
    {
        Board_Setup,
        Game_Start,
        Turn_Start,
        Turn_Pick_Piece,
        Turn_Move_Piece,
        Turn_Decision_Making,
        Turn_End,
        Remove_Piece,
        Game_End,
        Final,
        Error
    }
}
