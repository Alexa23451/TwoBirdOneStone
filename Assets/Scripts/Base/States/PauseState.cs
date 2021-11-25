using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IState
{
    public PauseState()
    {
        UIManager.Instance.GetPanel<PausePanel>().OnSettingGame += OnSettingGame;
        UIManager.Instance.GetPanel<PausePanel>().OnMainMenuGame += OnMainMenuGame;

    }

    public void Enter()
    {
        UIManager.Instance.ShowPanelWithDG(typeof(PausePanel) , ()=>Time.timeScale = 0);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        UIManager.Instance.HidePanelWithDG(typeof(PausePanel));
    }

    public void OnSettingGame()
    {
        //display setting panel
    }

    public void OnMainMenuGame()
    {
        GameplayController.Instance.NormalState();
        SceneController.Instance.ChangeScene(1);
    }
}
