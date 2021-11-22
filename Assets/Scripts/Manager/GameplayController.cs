using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameplayController : BaseManager<GameplayController>
{
    private StateController _stateController;
    public event Action OnStartLv;

    public override void Init()
    {
        _stateController = new StateController(new Dictionary<Type, IState>
        {
            { typeof(StartLevelState), new StartLevelState() },
            { typeof(Win1GameState), new Win1GameState() },
            { typeof(LoseState), new LoseState() },
            { typeof(PauseState), new PauseState() },
            { typeof(NormalState), new NormalState() },
            { typeof(AdsState), new AdsState() },
        });

        SetEvent();

        SceneController.Instance.NextScene(1);
    }

    private void OnLoadScene(int sceneId)
    {
        if (sceneId > 1)
        {
            SoundManager.Instance.PlaySoundIfNotPlay(Sounds.LevelBGM, true, true, true);

            int lv = SceneManager.GetActiveScene().buildIndex - 1;

            if (lv > DataManager.Instance.UnlockLv)
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
        else
        {
            UIManager.Instance.HideAllPanel();
        }
    }

    private void OnDestroy()
    {
        SceneController.Instance.OnChangeScene -= OnLoadScene;
        //UIManager.Instance.GetPanel<WinLvPanel>().OnNextLv -= _stateController.GetState<Win1GameState>().OnNexLv;
        //UIManager.Instance.GetPanel<PlayAgainPanel>().OnNoTks -= _stateController.GetState<LoseState>().OnPlayAgain;

    }

    private void SetEvent()
    {
        SceneController.Instance.OnChangeScene += OnLoadScene;

        UIManager.Instance.GetPanel<WinLvPanel>().OnNextLv += () => {

            if (DataManager.Instance.CurrentLv == GlobalSetting.Instance.totalLevel)
            {
                UIManager.Instance.HidePanelWithDG(typeof(WinLvPanel));
                UIManager.Instance.ShowPanelWithDG(typeof(BetaTestPanel));
                return;
            }

            //Load inter ads
            if (DataManager.Instance.CurrentLv % 3 == 0)
            {
                if (DataManager.Instance.RemoveAdsOn)
                {
                    _stateController.GetState<Win1GameState>().OnNexLv();
                    SoundManager.Instance.Play(Sounds.UI_POPUP);
                }
                else
                {
                    if (Application.internetReachability != NetworkReachability.NotReachable)
                    {
                        AdmobController.Instance.ShowInterstitial(() =>
                        {
                            _stateController.GetState<Win1GameState>().OnNexLv();
                            SoundManager.Instance.Play(Sounds.UI_POPUP);
                        });
                    }
                    else
                    {
                        UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
                    }
                }
            }
            else
            {
                _stateController.GetState<Win1GameState>().OnNexLv();
                SoundManager.Instance.Play(Sounds.UI_POPUP);
            }
        };
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnNoTks += _stateController.GetState<LoseState>().OnPlayAgain;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds += _stateController.GetState<AdsState>().OnPlayAgain;
        UIManager.Instance.GetPanel<GameplayPanel>().OnStopMenu += PauseState;
        UIManager.Instance.GetPanel<PausePanel>().OnResumeGame += NormalState;
        UIManager.Instance.GetPanel<PausePanel>().OnSettingGame += _stateController.GetState<PauseState>().OnSettingGame;
        UIManager.Instance.GetPanel<PausePanel>().OnMainMenuGame += _stateController.GetState<PauseState>().OnMainMenuGame;
    }

    public T GetState<T>() where T : class, IState => _stateController.GetState<T>();
    public void InitLevelState() => _stateController.SetState<StartLevelState>();
    public void WinLevelState() => _stateController.SetState<Win1GameState>();
    public void LoseLevelState() => _stateController.SetState<LoseState>();
    public void PauseState() => _stateController.SetState<PauseState>();
    public void NormalState() => _stateController.SetState<NormalState>();
    public void AdsState() {
        _stateController.SetState<AdsState>();
    }

    public IState GetCurrentState() => _stateController.CurrentState;

    public Type GetCurrentType() => _stateController.CurrentTypeState;
}
