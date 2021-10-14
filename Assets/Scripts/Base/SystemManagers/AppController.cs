using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.Init();
        GameplayController.Instance.Init();
        CameraController.Instance.Init();
        SoundManager.Instance.Init();
        TimerManager.Instance.Init();
    }

    
}
