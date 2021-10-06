using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextManager
{
    private string _path;
    private Canvas _canvas;
    private Dictionary<Type, PopUpText> _popUpTextPrefabs;    // TO DO: change PopUpText to Interface

    public PopUpTextManager(Canvas canvas, string path = "UI")
    {
        _path = path;
        _canvas = canvas;
        _popUpTextPrefabs = new Dictionary<Type, PopUpText>();

        LoadPopUpTextPrefabs();
    }

    private void LoadPopUpTextPrefabs()
    {
        foreach (var pupUpText in Resources.LoadAll<PopUpText>(_path))
        {
            _popUpTextPrefabs.Add(pupUpText.GetType(), pupUpText);
        }
    }

    public void AddPopUpText<T>(string text, Color color) where T : PopUpText
    {
        if (_popUpTextPrefabs.ContainsKey(typeof(T)))
        {
            GameObject prefab = _popUpTextPrefabs[typeof(T)].gameObject;
            GameObject popUpTextGameObject = GameObject.Instantiate(prefab, _canvas.transform);
            PopUpText popUpText = popUpTextGameObject.GetComponent<PopUpText>();
            popUpText.SetText(text, color);
        }
    }
}
