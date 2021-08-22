using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneInvocation : MonoBehaviour
{
    private void Awake()
    {
        OnSceneInitialize();
    }
    private void Start()
    {
        StartScene();
    }

    private void OnDestroy()
    {
        OnObjDestroy();
    }
    protected abstract void OnSceneInitialize();
    protected abstract void StartScene();
    protected abstract void OnObjDestroy();
}

