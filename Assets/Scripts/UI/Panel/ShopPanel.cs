using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : BasePanel
{
    public void ClosePanel()
    {
        HideWithDG();
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }


    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
