public interface IState
{
    public void Process();

    public bool IsFinalState();

    public State GetState();

    public enum State
    {
        Board_Setup,
        Setup_Highlight_Empty_Spaces,
        Setup_Place_Piece,
        Setup_Check_Setup_End,
        Setup_Post_Setup,
        Game_Start,
        Turn_Start,
        Turn_Pick_Piece,
        Turn_Move_Piece,
        Turn_Decision_Making,
        Turn_End,
        Game_End,
        Remove_Piece,
        Final,
        Error
    }
}
