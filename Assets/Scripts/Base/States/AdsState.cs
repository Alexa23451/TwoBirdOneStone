using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsState : IState
{
    public void Enter()
    {
        Time.timeScale = 0;

        if (DataManager.Instance.RemoveAdsOn)
        {
            GameplayController.Instance.NormalState();
            return;
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            AdmobController.Instance.ShowRewardedAd(
                () =>
                {
                    GameplayController.Instance.NormalState();
                },
                () =>
                {
                    UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
                });
        }
        else
        {
            UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
        }
    }

    public void OnPlayAgain()
    {
        if (DataManager.Instance.RemoveAdsOn)
        {
            UIManager.Instance.GetPanel<PlayAgainPanel>().HideWithDG();
            GameplayController.Instance.NormalState();
            return;
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            AdmobController.Instance.ShowRewardedAd(
            ()=> {
                UIManager.Instance.GetPanel<PlayAgainPanel>().HideWithDG();
                GameplayController.Instance.NormalState();
            }, 
            ()=>
            {   
                UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
            });
        }
        else
        {
            UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
        }
    }

    public void OnGetBonusMoney(int moneyAmount)
    {
        if (DataManager.Instance.RemoveAdsOn)
        {
            UIManager.Instance.GetPanel<BonusPanel>().HideWithDG();
            DataManager.Instance.Money += moneyAmount;
            Debug.Log("GET REWARD WITHOUT WATCH ADS");
            return;
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            AdmobController.Instance.ShowRewardedAd(
            () => {
                UIManager.Instance.GetPanel<BonusPanel>().HideWithDG();
                DataManager.Instance.Money += moneyAmount;
            },
            () =>
            {
                UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
            });
        }
        else
        {
            UIManager.Instance.ShowPanelWithDG(typeof(AdsNotReadyPanel));
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
    }
}
