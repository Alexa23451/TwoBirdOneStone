using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Base class for any UI panel (Menus, Popups etc)
/// </summary>
/// 
[RequireComponent(typeof(RectTransform))]
public abstract class BasePanel : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void ShowWithDG(Action OnAfterShow = null)
    {
        gameObject.SetActive(true);

        if (rect != null)
            rect.anchoredPosition = Vector2.zero;
        transform.DOKill();

        transform.localScale = Vector2.one * 0.15f;
        transform.DOScale(1, 0.5f).SetEase(Ease.InOutBack).OnComplete( ()=> OnAfterShow?.Invoke() );
    }

    public void Show()
    {
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void HideWithDG()
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();

        transform.DOKill();
        transform.DOScale(0f, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() => gameObject.SetActive(false));
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
