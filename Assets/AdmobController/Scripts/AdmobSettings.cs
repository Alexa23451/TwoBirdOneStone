using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName ="Dashbit/New Admob Settings")]
public class AdmobSettings : ScriptableObject
{
    [System.Flags]
    private enum AdFlags
    {
        Use_TestAd = 1 << 0,
        Enable_Banner = 1 << 1,
        Enable_Interstitial = 1 << 2,
        Enable_Rewarded = 1 << 3,
        Use_Mediation_Ad = 1 << 4,

        AutoRefresh_Banner = Enable_Banner | 1 << 7,
        AutoRefresh_Interstitial = Enable_Interstitial | 1 << 8,
        AutoRefresh_Rewarded = Enable_Rewarded | 1 << 9,
    }

    private static readonly string adId_test_banner = "ca-app-pub-3940256099942544/6300978111";
    private static readonly string adId_test_interstitial = "ca-app-pub-3940256099942544/1033173712";
    private static readonly string adId_test_rewarded = "ca-app-pub-3940256099942544/5224354917";
    private static readonly string adId_test_native = "ca-app-pub-3940256099942544/2247696110";

    [SerializeField] private AdFlags flags;

    [SerializeField] private string adId_banner;
    [SerializeField] private string adId_interstitial;
    [SerializeField] private string adId_rewarded;

    [SerializeField] private AdPosition bannerAdPosition;

    [SerializeField] private float autoRefresh_banner;
    [SerializeField] private float autoRefresh_interstitial;
    [SerializeField] private float autoRefresh_rewarded;
    
    public bool UseTestAd => flags.HasFlag(AdFlags.Use_TestAd);
    public bool EnableBanner => flags.HasFlag(AdFlags.Enable_Banner);
    public bool EnableInterstitial => flags.HasFlag(AdFlags.Enable_Interstitial);
    public bool EnableRewarded => flags.HasFlag(AdFlags.Enable_Rewarded);
    public bool UseAdMediation => flags.HasFlag(AdFlags.Use_Mediation_Ad);

    public string BannerId => UseTestAd ? adId_test_banner : adId_banner;
    public string InterstitialId => UseTestAd ? adId_test_interstitial : adId_interstitial;
    public string RewardedId => UseTestAd ? adId_test_rewarded : adId_rewarded;

    public AdPosition BannerAdPosition => bannerAdPosition;

    public bool AutoRefreshBanner => flags.HasFlag(AdFlags.AutoRefresh_Banner);
    public bool AutoRefreshInterstitial => flags.HasFlag(AdFlags.AutoRefresh_Interstitial);
    public bool AutoRefreshRewarded => flags.HasFlag(AdFlags.AutoRefresh_Rewarded);

    public float BannerRefreshTime => autoRefresh_banner;
    public float InterstitialRefreshTime => autoRefresh_interstitial;
    public float RewardedRefreshTime => autoRefresh_rewarded;
}
