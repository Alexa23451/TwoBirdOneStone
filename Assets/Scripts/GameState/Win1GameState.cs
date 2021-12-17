using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Win1GameState : IState
{
    public Action<int> OnWinGame;

    public Win1GameState()
    {

    }

    public void Enter()
    {
        Debug.Log("WIN GAME");

        OnWinGame?.Invoke(DataManager.Instance.CurrentLv);
        int moneyBonus = LevelData.Instance.moneyRewardOnLevel[DataManager.Instance.CurrentLv - 1];
        DataManager.Instance.Money += moneyBonus;
        
        TimerManager.Instance.AddTimer(1f,() => { 
            UIManager.Instance.ShowPanelWithDG(typeof(WinLvPanel));
            SoundManager.Instance.Play(Sounds.WIN_LV);
        });
    }

    public void OnNexLv()
    {
        SceneController.Instance.NextScene();
    }

    public void Exit()
    {
        UIManager.Instance.HidePanelWithDG(typeof(WinLvPanel));
    }
}
