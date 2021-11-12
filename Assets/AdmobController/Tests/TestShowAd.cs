using GoogleMobileAdsMediationTestSuite.Api;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestShowAd : MonoBehaviour
{
    [SerializeField] private AdmobController admobController;
    [SerializeField] private Button btn_showHideBanner;
    [SerializeField] private Button btn_showInterstitialAd;
    [SerializeField] private Button btn_showRewardedAd;
    [SerializeField] private Button btn_testSuite;
    [SerializeField] private Text text_status;

    private void Start()
    {
        btn_showHideBanner.onClick.AddListener(ShowBanner);
        btn_showInterstitialAd.onClick.AddListener(ShowInterstitialAd);
        btn_showRewardedAd.onClick.AddListener(ShowRewarded);
        btn_testSuite.onClick.AddListener(ShowTestSuite);

        StartCoroutine(SlowUpdateCoroutine());
    }

    IEnumerator SlowUpdateCoroutine(float period = .1f)
    {
        var wft = new WaitForSeconds(period);

        while (true)
        {
            yield return wft;
            SlowUpdate();
            UpdateUI();
        }
    }

    private void SlowUpdate()
    {
        btn_showHideBanner.interactable = admobController.IsBannerLoaded;
        btn_showInterstitialAd.interactable = admobController.IsInterstitialAdLoaded;
        btn_showRewardedAd.interactable = admobController.IsRewardedAdLoaded;
    }

    private void UpdateUI()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Debug line:");
        sb.AppendLine($"Controller : {admobController.ControlFlags}");
        sb.AppendLine($"Banner : {admobController.BannerFlags}");
        sb.AppendLine($"Interstitial : {admobController.InterstitialFlags}");
        sb.AppendLine($"Rewarded : {admobController.RewardedFlags}");

        text_status.text = sb.ToString();
    }

    private bool bannerShow = true;
    public void ShowBanner()
    {
        bannerShow = !bannerShow;
        admobController.ShowBanner(bannerShow);        
    }

    public void ShowInterstitialAd()
    {
        admobController.ShowInterstitial(()=>
        {
            Debug.Log("Oke done interstitial");
        });
    }

    public void ShowRewarded()
    {
        admobController.ShowRewardedAd(()=>
        {
            Debug.Log("Oke user get rewards");

        }, () => Debug.Log("ADS FAILED TO SHOW"));
    }

    public void ShowTestSuite()
    {
        MediationTestSuite.Show();
    }
}
