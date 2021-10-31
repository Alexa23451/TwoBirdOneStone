using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.Init();
        UIManager.Instance.Init();
        GameplayController.Instance.Init();
        TimerManager.Instance.Init();
        DataManager.Instance.Init();
    }

    
}
