using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            CreateInstance();
            return _instance;
        }
    }

    public static void CreateInstance()
    {
        if (_instance == null)
        {
            //find existing instance
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                //create new instance
                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }
            //initialize instance if necessary
            if (!_instance.initialized)
            {
                _instance.Initialize();
                _instance.initialized = true;
            }
        }
    }

    public virtual void Awake()
    {
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(this);
        }        

        //check if instance already exists when reloading original scene
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    protected bool initialized;

    protected virtual void Initialize() { }
}

public enum RewardType
{
    Nothing , Money, GasFill , TruckTow, Speed
}

public interface IAdmobService
{
    void ShowRewardedAd(System.Action onUserClaimed, System.Action onUserAdsFailed);
    void ShowInterstitial(System.Action onAdClose);
}

public interface IAdmobCallback
{
    //INTERSTITIAL
    event Action OnInterstitialOpened;
    event Action OnInterstitialClosed;
    event Action OnInterstitialLoaded;
    event Action OnInterstitialFailToLoad;

    //REWARDED
    event Action OnRewardedClosed;
    event Action OnRewardedLoaded;
    event Action<RewardType,float> OnRewardedUserEarned;
    event Action OnRewardedFailedToShow;
    event Action OnRewardedFailedToLoad;

    //NATIVE
    event Action OnNativeLoaded;
    event Action OnNativeClicked;
    event Action OnNativeFailedToLoad;
}

public class AdmobController : BaseManager<AdmobController> , IAdmobCallback, IAdmobService
{
    [System.Flags]
    public enum ControllerFlags
    {
        AUTO_INIT = 1 << 0,
        INIT_AT_START = AUTO_INIT | 1 << 1,
        INIT_AT_AWAKE = AUTO_INIT | 1 << 2,

        INITIALIZING = 1 << 10,
        INITIALIZED = INITIALIZING | 1 << 11,
    }
    [System.Flags]
    public enum AdFlags
    {
        NOT_LOADED = 0,
        LOADING = 1 << 0,
        LOADED = 1 << 1,
        FAILED = 1 << 2
    }
    [SerializeField] private AdmobSettings settings;
    [SerializeField] private ControllerFlags controlFlags;

    private volatile Queue<System.Action> delayedTasks = new Queue<System.Action>();

    public bool AutoInit => controlFlags.HasFlag(ControllerFlags.AUTO_INIT);

    public bool Initialized => controlFlags.HasFlag(ControllerFlags.INITIALIZED);

    //public override void Awake()
    //{
    //    base.Awake();
    //    if (controlFlags.HasFlag(ControllerFlags.INIT_AT_AWAKE))
    //    {
    //        Init();
    //    }
    //}

    private void Start()
    {
        if(controlFlags.HasFlag(ControllerFlags.INIT_AT_START))
        {
            Init();
        }
    }
    

    public override void Init()
    {
        if (Initialized)
            return;

        if (settings.UseTestAd || !settings.UseAdMediation)
        {
            Debug.Log("Don't use ad mediation");
            MobileAds.DisableMediationInitialization();
        }
        //Debug.Log("Ads initializing");
        controlFlags |= ControllerFlags.INITIALIZING;
        MobileAds.Initialize(MobileAdsInitializationCallback);


        //register
        Debug.LogError("ON REGISTER ADMOB");
        Services.RegisterAs<IAdmobService>(this);
        Services.RegisterAs<IAdmobCallback>(this);

        //quickly load the ads as possible
        //controlFlags |= ControllerFlags.INITIALIZED;
        //controlFlags &= ~ControllerFlags.INITIALIZED;

        //controlFlags |= ControllerFlags.INITIALIZED;

        //StartCoroutine(CallFirstLoading());

        //IEnumerator CallFirstLoading()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    FirstLoadingAds();
        //}
    }    

    private void MobileAdsInitializationCallback(InitializationStatus status)
    {
       // Debug.Log("Ads Initialized!");
        controlFlags |= ControllerFlags.INITIALIZED;

        foreach (var kv in status.getAdapterStatusMap())
        {
            var name = kv.Key;
            var adpt = kv.Value;
             Debug.Log($"Adapter {name} load state : {adpt.InitializationState} | desc : {adpt.Description}");
        }
        //FirstLoadingAds();

        LoadBanner();
        LoadInterstitial();
        LoadRewarded();
    }

    private void FirstLoadingAds()
    {
        LoadBanner(true);
        LoadInterstitial(true);
        LoadRewarded(true);
    }

    private void Update()
    {
        if(delayedTasks.Count > 0)
        {
            var dequeue = delayedTasks.Dequeue();
            if (dequeue != null)
            {
                dequeue.Invoke();
            }
        }
    }

    private void LateUpdate()
    {
        AutoRefreshBanner();
        AutoRefreshInterstitial();
        AutoRefreshRewarded();
    }

    private AdRequest adRequest;

    private void LoadAdRequest()
    {
        if (adRequest != null)
            return;

        adRequest = new AdRequest.Builder().Build();
    }

    #region banner
    [SerializeField] private AdFlags bannerFlags = AdFlags.NOT_LOADED;
    private BannerView banner;
    private void LoadBanner(bool forced = false)
    {
        if (!settings.EnableBanner)
            return;
        if(!forced)
        {
            if (!controlFlags.HasFlag(ControllerFlags.INITIALIZED))
                return;
            if (bannerFlags.HasFlag(AdFlags.LOADED) || bannerFlags.HasFlag(AdFlags.LOADING))
                return;
        }

        if(banner!=null)
        {
            banner.Destroy();
        }

        banner = new BannerView(settings.BannerId, AdSize.Banner, settings.BannerAdPosition);

        LoadAdRequest();

        banner.OnAdLoaded += Banner_OnAdLoaded;
        banner.OnAdFailedToLoad += Banner_OnAdFailedToLoad;

        bannerFlags |= AdFlags.LOADING;

        banner.LoadAd(adRequest);
    }

    private void AutoRefreshBanner()
    {
        if (!settings.AutoRefreshBanner)
            return;


    }

    private void Banner_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {        
        bannerFlags = AdFlags.FAILED;
    }

    private void Banner_OnAdLoaded(object sender, System.EventArgs e)
    {
        bannerFlags |= AdFlags.LOADED;
    }
    #endregion

    #region interstitial
    [SerializeField] private AdFlags interstitialFlags = AdFlags.NOT_LOADED;
    private InterstitialAd interstitialAd;
    private System.Action interstitialAdCloseCallback;
    private float interstitialRefreshTimer;

    private void LoadInterstitial(bool forced = false)
    {
        if (!settings.EnableInterstitial)
            return;
        if(!forced)
        {
            if (!controlFlags.HasFlag(ControllerFlags.INITIALIZED))
                return;
            if (interstitialFlags.HasFlag(AdFlags.LOADED) || interstitialFlags.HasFlag(AdFlags.LOADING))
                return;
        }

      //  Debug.Log("Interstitial requested");
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        interstitialAd = new InterstitialAd(settings.InterstitialId);

        LoadAdRequest();

        interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;        
        interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
        interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;        

        if(interstitialFlags == AdFlags.FAILED)
        {
            interstitialFlags = AdFlags.NOT_LOADED;
        }

        interstitialFlags |= AdFlags.LOADING;

        interstitialAd.LoadAd(adRequest);
    }
    private void AutoRefreshInterstitial()
    {
        if (!settings.AutoRefreshInterstitial)
            return;

        if (!controlFlags.HasFlag(ControllerFlags.INITIALIZED))
            return;

        if (interstitialFlags.HasFlag(AdFlags.LOADED) || interstitialFlags.HasFlag(AdFlags.LOADING))
        {
            interstitialRefreshTimer = 0;
            return;
        }            


        interstitialRefreshTimer += Time.deltaTime;
        if (interstitialRefreshTimer < settings.InterstitialRefreshTime)
            return;


        LoadInterstitial();
    }
    private void InterstitialAd_OnAdClosed(object sender, System.EventArgs e)
    {
        interstitialFlags = AdFlags.NOT_LOADED;
        if(interstitialAdCloseCallback != null)
        {
            delayedTasks.Enqueue(() =>
            {
                interstitialAdCloseCallback?.Invoke();
                interstitialAdCloseCallback = null;
            });

        }        
    }

    private void InterstitialAd_OnAdLoaded(object sender, System.EventArgs e)
    {
       // Debug.Log("Interstitial Ad Loaded");
        interstitialFlags |= AdFlags.LOADED;
    }

    private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        //Debug.Log("Interstitial Failed to Load");
        interstitialFlags = AdFlags.FAILED;
    }
    #endregion

    #region rewarded ads
    [SerializeField] private AdFlags rewardedFlags = AdFlags.NOT_LOADED;
    private RewardedAd rewardedAd;
    private System.Action rewardedAdUserClaimedCallback;
    private float rewardedRefreshTimer;

    public event Action OnInterstitialOpened;
    public event Action OnInterstitialClosed;
    public event Action OnInterstitialLoaded;
    public event Action OnInterstitialFailToLoad;
    public event Action OnRewardedClosed;
    public event Action OnRewardedLoaded;
    public event Action<RewardType , float> OnRewardedUserEarned;
    public event Action OnRewardedFailedToShow;
    public event Action OnRewardedFailedToLoad;
    public event Action OnNativeLoaded;
    public event Action OnNativeClicked;
    public event Action OnNativeFailedToLoad;

    private void LoadRewarded(bool forced = false)
    {
        if (!settings.EnableRewarded)
            return;
        if (!forced)
        {
            if (!controlFlags.HasFlag(ControllerFlags.INITIALIZED))
                return;
            if (rewardedFlags.HasFlag(AdFlags.LOADED) || rewardedFlags.HasFlag(AdFlags.LOADING))
                return;
        }
        Debug.Log("Rewarded requested");

        rewardedAd = new RewardedAd(settings.RewardedId);
        rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward; // cai treuong hop nay la dc nhan 
        rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
        rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow; // khong dc nhan

        rewardedFlags |= AdFlags.LOADING;

        LoadAdRequest();
        rewardedAd.LoadAd(adRequest);

    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        Debug.LogError("Ad failed to show: " + e.Message);
        rewardedFlags = AdFlags.FAILED;
        OnRewardedFailedToShow?.Invoke();
        OnRewardedFailedToShow = null;
    }

    private void AutoRefreshRewarded()
    {
        if (!settings.AutoRefreshRewarded)
            return;

        if (!controlFlags.HasFlag(ControllerFlags.INITIALIZED))
            return;

        if (rewardedFlags.HasFlag(AdFlags.LOADED) || rewardedFlags.HasFlag(AdFlags.LOADING))
        {
            rewardedRefreshTimer = 0;
            return;
        }

        rewardedRefreshTimer += Time.deltaTime;
        if (rewardedRefreshTimer < settings.RewardedRefreshTime)
            return;

        LoadRewarded();
    }
    private void RewardedAd_OnAdClosed(object sender, System.EventArgs e)
    {
        Debug.Log("RewardedAd_OnAdClosed");
        rewardedFlags = AdFlags.NOT_LOADED;
        OnRewardedClosed?.Invoke();
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        Debug.Log("RewardedAd_OnUserEarnedReward");

        if (rewardedAdUserClaimedCallback != null)
        {
            delayedTasks.Enqueue(() =>
            {
                rewardedAdUserClaimedCallback.Invoke();
                rewardedAdUserClaimedCallback = null;
            });
        }
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("RewardedAd_OnAdFailedToLoad");
        rewardedFlags &= ~(AdFlags.LOADED | AdFlags.LOADING);
        rewardedFlags |= AdFlags.NOT_LOADED;
        OnRewardedFailedToLoad?.Invoke();
    }

    private void RewardedAd_OnAdLoaded(object sender, System.EventArgs e)
    {
        Debug.Log("RewardedAd_OnAdLoaded");
        rewardedFlags |= AdFlags.LOADED;
        OnRewardedLoaded?.Invoke();
    }
    #endregion

    #region open API
    public bool IsBannerLoaded => banner != null && bannerFlags.HasFlag(AdFlags.LOADED);
    public void ShowBanner(bool visible = true)
    {
        if (!IsBannerLoaded)
            return;

        if (visible)
            banner.Show();
        else
            banner.Hide();
    }

    public bool IsInterstitialAdLoaded
    {
        get
        {
         //   Debug.Log("Interstital Ad Status : " + interstitialFlags);
            return interstitialAd != null && interstitialAd.IsLoaded();
        }
    }    

    public void ShowInterstitial(System.Action onAdClose)
    {
      //  Debug.Log("Interstital Ad Status : " + interstitialFlags);
        if(!IsInterstitialAdLoaded)
        {
            onAdClose?.Invoke();
            return;
        }

        if(onAdClose != null)
        {
            interstitialAdCloseCallback = onAdClose;
        }

        interstitialAd.Show();
    }

    public IEnumerator ShowInterstitialCoroutine()
    {
        if (!IsInterstitialAdLoaded)
            yield break;

        interstitialAd.Show();

        yield return new WaitUntil(() => interstitialFlags.HasFlag(AdFlags.NOT_LOADED));
        yield return new WaitUntil(() => !interstitialAd.IsLoaded());
    }

    public bool IsRewardedAdLoaded => rewardedAd != null && rewardedAd.IsLoaded();
    public void ShowRewardedAd(System.Action onUserClaimed, Action onUserAdsFailed = null)
    {
       Debug.Log("Rewarded Ad Status : " + rewardedFlags);
        if (!IsRewardedAdLoaded)
            return;

        if(onUserClaimed!=null)
        {
            rewardedAdUserClaimedCallback = onUserClaimed;
        }

        if (onUserAdsFailed != null)
        {
            OnRewardedFailedToShow = onUserAdsFailed;
        }

        rewardedAd.Show();
    }
    #endregion

    #region exposed properties
    public ControllerFlags ControlFlags => controlFlags;
    public AdFlags BannerFlags => bannerFlags;
    public AdFlags InterstitialFlags => interstitialFlags;
    public AdFlags RewardedFlags => rewardedFlags;
    #endregion
}
