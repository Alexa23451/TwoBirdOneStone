using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameplayController : BaseManager<GameplayController>
{
    private StateController _stateController;
    public event Action OnStartLv;
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

            int lv = SceneManager.GetActiveScene().buildIndex - 1;

            if(lv > DataManager.Instance.UnlockLv)
            {
                DataManager.Instance.UnlockLv = lv;
            }

            DataManager.Instance.CurrentLv = lv;
            DataManager.Instance.SaveGame();

            UIManager.Instance.HideAllPanel();
            UIManager.Instance.ShowPanel(typeof(GameplayPanel));
            UIManager.Instance.GetPanel<GameplayPanel>().SetTextLv("Level " + lv.ToString());
            OnStartLv?.Invoke();
            InitLevelState();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= InitGame;
        //UIManager.Instance.GetPanel<WinLvPanel>().OnNextLv -= _stateController.GetState<Win1GameState>().OnNexLv;
        //UIManager.Instance.GetPanel<PlayAgainPanel>().OnNoTks -= _stateController.GetState<LoseState>().OnPlayAgain;

    }

    private void SetEvent()
    {
        SceneManager.sceneLoaded += InitGame;
        UIManager.Instance.GetPanel<WinLvPanel>().OnNextLv += _stateController.GetState<Win1GameState>().OnNexLv;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnNoTks += _stateController.GetState<LoseState>().OnPlayAgain;
    }

    public T GetState<T>() where T: class, IState => _stateController.GetState<T>();
    public void InitLevelState() => _stateController.SetState<StartLevelState>();
    public void WinLevelState() => _stateController.SetState<Win1GameState>();
    public void LoseLevelState() => _stateController.SetState<LoseState>();

    public IState GetCurrentState() => _stateController.CurrentState;

    public Type GetCurrentType() => _stateController.CurrentTypeState;
}
