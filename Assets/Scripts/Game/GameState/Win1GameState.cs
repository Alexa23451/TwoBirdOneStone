using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Win1GameState : IState
{
    ISceneManagement sceneManagement;

    public Win1GameState()
    {
        Services.Find(out sceneManagement);
    }

    public void Enter()
    {
        Debug.Log("WIN GAME");

        DataManager.Instance.Money += GlobalSetting.Instance.moneyRewardOnLevel[DataManager.Instance.CurrentLv - 1];
        
        TimerManager.Instance.AddTimer(1f,() => { 
            UIManager.Instance.ShowPanelWithDG(typeof(WinLvPanel));
            SoundManager.Instance.Play(Sounds.WIN_LV);
        });
    }

    public void OnNexLv()
    {
        sceneManagement.NextScene();
    }

    public void Exit()
    {
        UIManager.Instance.HidePanelWithDG(typeof(WinLvPanel));
    }
}
