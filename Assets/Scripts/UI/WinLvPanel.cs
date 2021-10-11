using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WinLvPanel : BasePanel
{
    public Button nextLvBtn;
    public event Action OnNextLv;

    private void Awake()
    {
        nextLvBtn.onClick.AddListener(NextLv);
    }

    private void NextLv()
    {
        OnNextLv?.Invoke();
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    
}
