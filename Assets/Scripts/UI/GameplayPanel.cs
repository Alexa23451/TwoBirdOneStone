using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : BasePanel
{
    public Button shotBtn;
    public Button stopGameBtn;
    public Slider moveSlider;
    public Text textLv;


    public float Speed => _speed;
    [SerializeField] private float _speed = 5f;

    public event Action<float> OnHorizontalUpdate;
    public event Action OnSpaceUpdate;
    public event Action OnStopMenu;

    private void Start()
    {
        shotBtn.onClick.AddListener(OnShotBtn);
        moveSlider.onValueChanged.AddListener(OnSliderChange);
        stopGameBtn.onClick.AddListener(OnStopBtnPress);
    }

    void OnEnable()
    {
        ResetSlider();
    }

    public void ResetSlider() => moveSlider.value = (moveSlider.minValue + moveSlider.maxValue) / 2;

    private void OnSliderChange(float value)
    {
        OnHorizontalUpdate?.Invoke(value);
    }

    private void OnStopBtnPress()
    {
        OnStopMenu?.Invoke();

        Services.Find(out ISceneManagement sceneManagement);
        UIManager.Instance.HideAllPanel();
        sceneManagement.ChangeScene(1);
    }

    private void OnShotBtn()
    {
        OnSpaceUpdate?.Invoke();
    }


    public override void OverrideText()
    {

    }
}
