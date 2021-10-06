using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : BasePanel, IPlayerInput
{
    public Button shotBtn;
    public Slider moveSlider;


    public float Speed => _speed;
    private float _speed = 5f;

    public event Action<float> OnHorizontalUpdate;
    public event Action OnSpaceUpdate;

    private void Awake()
    {
        Services.RegisterAs<IPlayerInput>(this);
    }

    private void OnDestroy()
    {
        Services.Unregister(this);
    }

    private void Start()
    {
        moveSlider.value = (moveSlider.minValue + moveSlider.maxValue) / 2;

        shotBtn.onClick.AddListener(OnShotBtn);
        moveSlider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnSliderChange(float value)
    {
        OnHorizontalUpdate?.Invoke(value);
    }

    private void OnShotBtn()
    {
        OnSpaceUpdate?.Invoke();
    }


    public override void OverrideText()
    {

    }
}
