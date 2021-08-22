using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TheDeveloper.AdvancedObjectPool;


public class TestTest : MonoBehaviour
{


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Services.Find(out ISceneManagement sceneManagement);
            sceneManagement.ChangeScene(1, 1);
        }

    }
}
