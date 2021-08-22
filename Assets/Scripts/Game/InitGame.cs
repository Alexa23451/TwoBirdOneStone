using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    ISceneManagement sceneManagement;

    void Start()
    {
        Services.Find(out sceneManagement);
        sceneManagement.ChangeScene(1);
    }

    
}
