using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Base class for any UI panel (Menus, Popups etc)
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public abstract void OverrideText();

    protected void OverrideTextInChildren(Component component, Message key)
    {
        Text childrenText = component.gameObject.GetComponentInChildren<Text>();
        //if (childrenText != null)
        //    childrenText.text = Localisation.GetString(key);
    }
}
