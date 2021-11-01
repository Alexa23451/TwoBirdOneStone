using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameplayPanel : BasePanel
{
    [SerializeField] private Button shotBtn;
    [SerializeField] private Button stopGameBtn;
    [SerializeField] private Button rePlayBtn;
    [SerializeField] private Image shotImg;
    [SerializeField] private Slider moveSlider;
    [SerializeField] private Text textLv;

    [SerializeField] private Color canShotableColor;
    [SerializeField] private Color notShotableColor;
    [SerializeField] private float timeChangeColor = 1f;


    public float Speed => _speed;
    [SerializeField] private float _speed = 5f;

    public event Action<float> OnHorizontalUpdate;
    public event Action OnSpaceUpdate;
    public event Action OnStopMenu;
    public event Action OnRePlayGame;

    private void Start()
    {
        shotBtn.onClick.AddListener(OnShotBtn);
        moveSlider.onValueChanged.AddListener(OnSliderChange);
        stopGameBtn.onClick.AddListener(OnStopBtnPress);
        rePlayBtn.onClick.AddListener(OnReplayBtn);
    }

    void OnEnable()
    {
        ResetSlider();
        shotImg.color = canShotableColor;
    }

    private void OnReplayBtn()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        OnRePlayGame?.Invoke();
        Services.Find(out ISceneManagement sceneManagement);
        sceneManagement.ReloadScene();

    }

    public void ResetSlider() => moveSlider.value = (moveSlider.minValue + moveSlider.maxValue) / 2;

    private void OnSliderChange(float value)
    {
        OnHorizontalUpdate?.Invoke(value);
    }

    private void OnStopBtnPress()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        OnStopMenu?.Invoke();
    }

    private void OnShotBtn()
    {
        shotImg.DOColor(notShotableColor, timeChangeColor);
        OnSpaceUpdate?.Invoke();
    }

    public void SetTextLv(string txt)
    {
        textLv.text = txt;
    }

    public void OnRecharge()
    {
        shotImg.DOColor(canShotableColor, timeChangeColor);
    }

    public void SetSlider(float value)
    {
        value = Mathf.Clamp01(value);
        moveSlider.value = value;
    }

    public override void OverrideText()
    {

    }
}
