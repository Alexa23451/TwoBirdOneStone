using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), DisallowMultipleComponent]
public abstract class BasePopUpWindow : MonoBehaviour, IPointerDownHandler
{
    public Action<BasePopUpWindow> OnRedyToDestroy;

    [Header("Main parametrs")]
    [SerializeField] protected float _timeToDestroy = 5f;
    [SerializeField, Range(0.01f, 1f)] protected float _hideSpeed = 0.5f;
    [SerializeField, Range(0f, 1f)] protected float _hideAlphaValue = 0.05f;

    [Header("FX sounds")]
    [SerializeField] protected Sounds _showSounds;
    [SerializeField] protected Sounds _touchSounds;

    [Header("Description")]
    [SerializeField] protected Text _message;
    [SerializeField] protected Image _image;

    protected bool _isStartHidding;

    public virtual void SetDescription(string message = "")
    {
        if(_message)
            _message.text = message;
    }

    public virtual void SetSprite(string imagePath = "")
    {
        if(_image && imagePath != "")
            _image.sprite = Resources.Load<Sprite>(imagePath);
    }

    private void Start()
    {
        StartCoroutine(ShowTimer());
        SoundManager.Instance.Play(_showSounds);        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!_isStartHidding)
        {
            SoundManager.Instance.Play(_touchSounds);

            StopAllCoroutines();
            HideWindow();
        }
    }

    protected virtual IEnumerator ShowTimer()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        HideWindow();
    }

    public void ForceDestroy() => OnRedyToDestroy?.Invoke(this);

    protected virtual void HideWindow()
    {
        _isStartHidding = true;

        if (gameObject.activeInHierarchy)
            StartCoroutine(HideWindowCoroutine());
    }

    protected virtual IEnumerator HideWindowCoroutine()
    {
        Image background = GetComponent<Image>();
        while(background.color.a > _hideAlphaValue)
        {
            Color newColor = background.color;
            newColor.a = Mathf.Clamp(newColor.a - _hideSpeed * Time.deltaTime, 0 , 1f);
            background.color = newColor;
            yield return null;
        }

        OnRedyToDestroy?.Invoke(this);
        yield break;
    }

}
