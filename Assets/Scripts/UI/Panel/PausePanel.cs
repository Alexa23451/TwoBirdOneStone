using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PausePanel : BasePanel, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        
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
        settingBtn.onClick.AddListener(OnSetting);
        mainMenuBtn.onClick.AddListener(OnMainMenu);
    }

    private void OnResume() => OnResumeGame?.Invoke();
    private void OnSetting() => OnSettingGame?.Invoke();
    private void OnMainMenu() => OnMainMenuGame?.Invoke();

    
}
