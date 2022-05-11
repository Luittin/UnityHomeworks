
interface IMenuController
{
    public void OnSelectMenu(State nextState);

    public void ChangeState(State newState);

    public void StopCurrentState();

    public void StartNewState();
}
