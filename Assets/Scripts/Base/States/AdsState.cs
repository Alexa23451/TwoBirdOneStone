using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsState : IState
{

    const string headerAds = "ADVERTISEMENT NOT READY YET";
    const string desAdsNoNet = "CHEATING detected !!!\n\nOpen your wifi, Watch some ads, or I will cry: (((((((((";
    const string desAds = "OOP !!!\n\n";

    public AdsState()
    {
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds += OnPlayAgain;

    }

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
                    UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAds);
                    UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
                });
        }
        else
        {
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAdsNoNet);
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
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
                UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAds);
                UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            });
        }
        else
        {
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAdsNoNet);
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
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
                UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAds);
                UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            });
        }
        else
        {
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo(headerAds, desAdsNoNet);
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
    }
}
