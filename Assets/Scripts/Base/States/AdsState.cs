using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsState : IState
{
    public void Enter()
    {
        Time.timeScale = 0;
        AdmobController.Instance.ShowRewardedAd(
            delegate {
                GameplayController.Instance.NormalState();
            });
    }

    public void OnPlayAgain()
    {
        AdmobController.Instance.ShowRewardedAd(
            delegate {
                UIManager.Instance.GetPanel<PlayAgainPanel>().HideWithDG();
                GameplayController.Instance.NormalState();
            });
    }

    public void Exit()
    {
        Time.timeScale = 1;
    }
}
