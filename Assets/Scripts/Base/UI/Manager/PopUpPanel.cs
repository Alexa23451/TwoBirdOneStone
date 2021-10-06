using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : BasePanel
{
    private List<BasePopUpWindow> _currentPopUpWindows;

    private void Awake()
    {
        _currentPopUpWindows = new List<BasePopUpWindow>();
    }

    private void OnDisable()
    {
        ClearWindows();
    }

    public void AttachWindow(BasePopUpWindow window)
    {
        window.transform.SetParent(transform);
        window.GetComponent<RectTransform>().localScale = Vector3.one;
        _currentPopUpWindows.Add(window);
        FXPopUpShow(window);
    }

    private void ClearWindows()
    {
        foreach (var window in _currentPopUpWindows)
            if (window)
                window.ForceDestroy();

        _currentPopUpWindows.Clear();
    }

    private void FXPopUpShow(BasePopUpWindow window)
    {
        // TO DO: panel FX if need
    }

    public void FXPopUpHide(BasePopUpWindow window)
    {
        // TO DO: panel FX if need
    }

    public override void OverrideText() { }
}
