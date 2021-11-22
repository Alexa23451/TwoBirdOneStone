using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class manages viewing any UI Panels
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    private const float TEXT_SHOW_DELAY = 0.5f;
    private const string POPUP_PATH = "PopUpUI";
    private const string PANEL_PATH = "UI";

    // Submanagers
    private UIPopUpManager m_popUpManager;
    private UIPanelManager m_panelManager;
    private PopUpTextManager m_popUpTextManager;

    private float _currentTextShowDelay;

    public override void Init()
    {
        m_panelManager = new UIPanelManager(transform, PANEL_PATH);
        m_popUpManager = new UIPopUpManager(GetPanel<PopUpPanel>(), POPUP_PATH);
        m_popUpTextManager = new PopUpTextManager(m_panelManager.MainCanvas, POPUP_PATH);
    }

    #region popUp
    public T AddPopUpWindow<T>() where T : BasePopUpWindow => m_popUpManager.AddWindow<T>();

    public void AddPopUpText<T>(string text, Color color) where T : PopUpText
    {
        TimerManager.Instance.AddTimer(_currentTextShowDelay, () => AddPopUpTextDelayed<T>(text, color));
        _currentTextShowDelay += TEXT_SHOW_DELAY;
    }

    private void AddPopUpTextDelayed<T>(string text, Color color) where T : PopUpText
    {
        m_popUpTextManager.AddPopUpText<T>(text, color);

        _currentTextShowDelay -= TEXT_SHOW_DELAY;
        if (_currentTextShowDelay < 0)
            _currentTextShowDelay = 0;
    }
    #endregion

    #region panel
    public void ShowPanelWithDG(Type type, Action OnAfterShow = null) => m_panelManager.ShowPanelWithDG(type,OnAfterShow);
    public void ShowPanel(Type type) => m_panelManager.ShowPanel(type);

    public void HidePanelWithDG(Type type) => m_panelManager.HidePanelWithDG(type);
    public void HidePanel(Type type) => m_panelManager.HidePanel(type);

    public void HideAllPanelWithDG() => m_panelManager.HideAllPanelWithDG();
    public void HideAllPanel() => m_panelManager.HideAllPanel();

    public T GetPanel<T>() where T : BasePanel => m_panelManager.GetPanel<T>();

    public void OverrideTexts() => m_panelManager.OverrideTexts();

    #endregion
}