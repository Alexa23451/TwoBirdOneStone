using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PausePanel : BasePanel, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        OnResume();
    }

    public Button resumeBtn;
    public Button mainMenuBtn;
    public Button settingBtn;

    public event Action OnResumeGame;
    public event Action OnMainMenuGame;
    public event Action OnSettingGame;

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        resumeBtn.onClick.AddListener(OnResume);
        //settingBtn.onClick.AddListener(OnSetting);
        mainMenuBtn.onClick.AddListener(OnMainMenu);
    }

    private void OnResume()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        OnResumeGame?.Invoke();
    }
    private void OnSetting() {

        SoundManager.Instance.Play(Sounds.UI_POPUP);
        OnSettingGame?.Invoke();
    } 
    private void OnMainMenu()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        OnMainMenuGame?.Invoke();
    }


    
}

