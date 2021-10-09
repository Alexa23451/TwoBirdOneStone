using System;

public class LoseState : IState
{
    ISceneManagement sceneManagement;
    public event Action OnLoseGame;

    public LoseState()
    {
        Services.Find(out sceneManagement);
    }

    public void Enter()
    {
        OnLoseGame?.Invoke();
        UIManager.Instance.HideAllPanel();
        sceneManagement.ReloadScene();
    }

    public void Exit()
    {

    }

}
