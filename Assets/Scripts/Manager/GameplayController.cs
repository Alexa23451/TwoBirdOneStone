using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameplayController : BaseManager<GameplayController>
{
    private StateController _stateController;
    ISceneManagement sceneManagement;

    public override void Init()
    {
        _stateController = new StateController(new Dictionary<Type, IState>
        {
            { typeof(StartLevelState), new StartLevelState() },
            { typeof(Win1GameState), new Win1GameState() },
            { typeof(LoseState), new LoseState() },
        });

        SetEvent();

        Services.Find(out sceneManagement);
        sceneManagement.NextScene(1);
    }

    private void InitGame(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            UIManager.Instance.ShowPanel(typeof(GameplayPanel));
            InitLevelState();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= InitGame;
    }

    private void SetEvent()
    {
        SceneManager.sceneLoaded += InitGame;
    }

    public T GetState<T>() where T: class, IState => _stateController.GetState<T>();
    public void InitLevelState() => _stateController.SetState<StartLevelState>();
    public void WinLevelState() => _stateController.SetState<Win1GameState>();
    public void LoseLevelState() => _stateController.SetState<LoseState>();
}
