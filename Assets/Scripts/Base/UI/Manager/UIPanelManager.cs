using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager
{
    public Canvas MainCanvas => m_Canvas;

    [SerializeField]
    private Dictionary<Type, BasePanel> m_Panels;

    private Canvas m_Canvas;

    public UIPanelManager(Transform parent, string path = "UI")
    {
        m_Canvas = GetCanvas();
        m_Canvas.transform.SetParent(parent);
        m_Canvas.sortingOrder = 1;

        m_Panels = new Dictionary<Type, BasePanel>();
        CreatePanelInstance(path);
    }

    internal void OverrideTexts()
    {
        foreach (var panel in m_Panels)
            panel.Value.OverrideText();
    }

    #region private
    private Canvas GetCanvas()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
            return canvas.GetComponent<Canvas>();
        else
            return CreateCanvas();
    }

    private Canvas CreateCanvas()
    {
        GameObject canvasGameObject = new GameObject("Canvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvasGameObject.AddComponent<SafeAreaUI>();

        var scaler = canvasGameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
        var raycaster = canvasGameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1242, 2208);
        scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        return canvas;
    }

    private void CreatePanelInstance(string path)
    {
        foreach (var panel in Resources.LoadAll<BasePanel>(path))
        {
            BasePanel currentPanel = GameObject.Instantiate(panel, m_Canvas.transform);
            m_Panels.Add(currentPanel.GetType(), currentPanel);
            currentPanel.Hide();
        }
    }
    #endregion

    #region public
    /// <summary>
    /// Show UI panel according to Panel Type with DG
    /// </summary>
    public void ShowPanelWithDG(Type type, Action OnAfterShow = null)
    {
        if (m_Panels.ContainsKey(type))
            m_Panels[type].ShowWithDG(OnAfterShow);
        else
            Debug.LogWarning("Panel is not contained");
    }

    /// <summary>
    /// Show UI panel according to Panel Type
    /// </summary>
    public void ShowPanel(Type type)
    {
        if (m_Panels.ContainsKey(type))
            m_Panels[type].Show();
        else
            Debug.LogWarning("Panel is not contained");
    }

    /// <summary>
    /// Hide UI panel according to Panel Type using DG
    /// </summary>
    public void HidePanelWithDG(Type type)
    {
        if (m_Panels.ContainsKey(type))
            m_Panels[type].HideWithDG();
        else
            Debug.LogWarning("Panel is not contained");
    }

    /// <summary>
    /// Hide UI panel according to Panel Type
    /// </summary>
    public void HidePanel(Type type)
    {
        if (m_Panels.ContainsKey(type))
            m_Panels[type].Hide();
        else
            Debug.LogWarning("Panel is not contained");
    }

    /// <summary>
    /// Hide all panel with DG
    /// </summary>
    /// <param name="type"></param>
    public void HideAllPanelWithDG()
    {
        foreach (var panel in m_Panels.Values)
            panel.HideWithDG();
    }

    /// <summary>
    /// Hide all panel
    /// </summary>
    /// <param name="type"></param>
    public void HideAllPanel()
    {
        foreach (var panel in m_Panels.Values)
            panel.Hide();
    }

    public T GetPanel<T>() where T : BasePanel
    {
        if (m_Panels.ContainsKey(typeof(T)))
            return m_Panels[typeof(T)] as T;
        else
        {
            Debug.LogWarning("Panel is not contained");
            return null;
        }
    }
    #endregion
}