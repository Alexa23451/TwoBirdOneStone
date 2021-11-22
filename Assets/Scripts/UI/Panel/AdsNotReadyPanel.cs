using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdsNotReadyPanel : BasePanel, IPointerDownHandler
{


    public void OnPointerDown(PointerEventData eventData)
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        HideWithDG();
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
