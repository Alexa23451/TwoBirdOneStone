using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class WinLvPanel : BasePanel
{
    [SerializeField] private Button nextLvBtn;
    [SerializeField] private Text moneyReward;
    public event Action OnNextLv;

    public float timeCount = 1.5f;
    private int startMoney = 0;

    private void Awake()
    {
        nextLvBtn.onClick.AddListener(NextLv);
    }

    private void NextLv()
    {
        OnNextLv?.Invoke();
    }

    private void OnEnable()
    {
        int money = GlobalSetting.Instance.moneyRewardOnLevel[DataManager.Instance.CurrentLv - 1];
        startMoney = 0;
        DOTween.To(() => startMoney, (x) => startMoney = x, money , timeCount);
    }

    private void Update()
    {
        moneyReward.text = startMoney.ToString() + "$";
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    
}
