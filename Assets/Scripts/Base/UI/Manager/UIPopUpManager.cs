using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUpManager
{
    private Dictionary<Type, BasePopUpWindow> _popUpWindowPrefabs;

    private PopUpPanel _popUpPanel;

    public UIPopUpManager(PopUpPanel panel, string path = "PopUpUI")
    {
        _popUpWindowPrefabs = new Dictionary<Type, BasePopUpWindow>();
        _popUpPanel = panel;
        LoadPrefabs(path);

        if (panel == null)
            Debug.LogError("PopUp panel does not exist!");
    }

    #region private
    private void LoadPrefabs(string path)
    {
        foreach (var window in Resources.LoadAll<BasePopUpWindow>(path))
            _popUpWindowPrefabs.Add(window.GetType(), window);
    }

    private void DestroyWindow(BasePopUpWindow window)
    {
        if (window != null)
        {
            window.OnRedyToDestroy -= DestroyWindow;
            GameObject.Destroy(window.gameObject);
        }
    }
    #endregion

    #region public
    public T AddWindow<T>() where T : BasePopUpWindow
    {
        if (!_popUpWindowPrefabs.ContainsKey(typeof(T)))
            return null;

        BasePopUpWindow window = GameObject.Instantiate(_popUpWindowPrefabs[typeof(T)]).GetComponent<BasePopUpWindow>();
        window.OnRedyToDestroy += DestroyWindow;
        _popUpPanel.AttachWindow(window);

        //UIManager.Instance.ShowPanel(typeof(PopUpPanel));

        return (T)window;
    }
    #endregion
}