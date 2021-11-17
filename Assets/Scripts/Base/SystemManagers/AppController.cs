using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    void Awake()
    {
        SoundManager.Instance.Init();
        SceneController.Instance.Init();
        UIManager.Instance.Init();
        GameplayController.Instance.Init();
        TimerManager.Instance.Init();
        DataManager.Instance.Init();
        UnityAndroidVibrator.Instance.Init();
    }

    
}
