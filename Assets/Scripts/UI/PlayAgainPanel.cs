using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayAgainPanel : BasePanel
{
    public Button WatchAdsBtn;
    public Button NoTksBtn;

    public event Action OnWatchAds;
    public event Action OnNoTks;

    public Image[] imageSprites;

    private const float countDown = 3.5f;
    [SerializeField] private float countTime;

    private void Awake()
    {
        WatchAdsBtn.onClick.AddListener(OnWatchAdBtn);
        NoTksBtn.onClick.AddListener(OnNoTksBtn);

        foreach(var img in imageSprites)
        {
            img.enabled = false;
        }
    }

    private void ActiveNumberSprite(int num)
    {
        for(int i=0; i<imageSprites.Length; i++)
        {
            imageSprites[i].enabled = (i + 1) == num;
        }
    }

    private void Update()
    {
        if(countTime > 0)
        {
            countTime -= Time.deltaTime;

            ActiveNumberSprite(Mathf.RoundToInt(countTime));

            if(countTime <= 0)
            {
                NoTksBtn.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        countTime = countDown;
        NoTksBtn.gameObject.SetActive(false);
    }


    private void OnWatchAdBtn()
    {
        OnWatchAds?.Invoke();
    }

    private void OnNoTksBtn()
    {
        OnNoTks?.Invoke();
    }

    public override void OverrideText()
    {
        throw new NotImplementedException();
    }
}
