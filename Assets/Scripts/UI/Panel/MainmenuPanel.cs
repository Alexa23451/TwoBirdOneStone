using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour 
{
    [SerializeField] private Text currentLv;
    [SerializeField] private Text playerMoney;
    [SerializeField] private Button vibrateSettingBtn;
    [SerializeField] private Button soundSettingBtn;
    [SerializeField] private Image vibrateSettingImg;
    [SerializeField] private Image soundSettingImg;
    [SerializeField] private Button infoSettingBtn;


    [SerializeField] private AudioSource bgmMainMenu;

    [SerializeField] private Color colorOn;
    [SerializeField] private Color colorOff;

    private void Awake()
    {
        vibrateSettingBtn.onClick.AddListener(OnVibrateSetting);
        soundSettingBtn.onClick.AddListener(OnSoundSetting);
        infoSettingBtn.onClick.AddListener(OnInfoSetting);
    }

    private void Start()
    {
        currentLv.text = "Level " + DataManager.Instance.CurrentLv.ToString();
        playerMoney.text = DataManager.Instance.Money.ToString();

        vibrateSettingImg.color = DataManager.Instance.VibrateOn ? colorOn : colorOff;
        soundSettingImg.color = DataManager.Instance.SoundOn ? colorOn : colorOff;

        SoundManager.Instance.StopAll(true);
        SoundManager.Instance.GLOBAL_ON = DataManager.Instance.SoundOn;

        bgmMainMenu.enabled = DataManager.Instance.SoundOn;
    }

    private void OnVibrateSetting()
    {
        DataManager.Instance.VibrateOn = !DataManager.Instance.VibrateOn;
        vibrateSettingImg.color = DataManager.Instance.VibrateOn ? colorOn : colorOff;
        if(DataManager.Instance.VibrateOn)
            UnityAndroidVibrator.Instance.VibrateForGivenDuration();

        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }

    private void OnSoundSetting()
    {
        DataManager.Instance.SoundOn = !DataManager.Instance.SoundOn;
        soundSettingImg.color = DataManager.Instance.SoundOn ? colorOn : colorOff;

        bgmMainMenu.enabled = DataManager.Instance.SoundOn;
        SoundManager.Instance.GLOBAL_ON = DataManager.Instance.SoundOn;
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }

    private void OnInfoSetting()
    {
        UIManager.Instance.ShowPanelWithDG(typeof(InfoPanel));
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }

}
