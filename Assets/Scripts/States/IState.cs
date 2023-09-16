public interface IState
{
    public void Process();

    public bool IsFinalState();
}
