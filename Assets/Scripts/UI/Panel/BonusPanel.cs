using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BonusPanel : BasePanel, IPointerDownHandler
{
    [SerializeField] Button watchAds;
    [SerializeField] int moneyWatchAdsBonus = 300;


    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        watchAds.onClick.AddListener(() => {

            GameplayController.Instance.GetState<AdsState>().OnGetBonusMoney(moneyWatchAdsBonus);
        });
    }

    public void Hiding()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        HideWithDG();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Hiding();
    }
}
