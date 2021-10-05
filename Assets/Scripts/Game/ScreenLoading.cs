using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLoading : MonoBehaviour
{
    ISceneManagement sceneManagement;

    void Start()
    {
        GameplayController.Instance.OnWinGame += NextLevel;
        GameplayController.Instance.OnLoseGame += ResetLevel;


        Services.Find(out sceneManagement);
        sceneManagement.NextScene(1);
    }

    private void OnDestroy()
    {
        GameplayController.Instance.OnWinGame -= NextLevel;
        GameplayController.Instance.OnLoseGame -= ResetLevel;
    }

    private void NextLevel()
    {
        sceneManagement.NextScene();
    }

    private void ResetLevel()
    {
        sceneManagement.ReloadScene();
    }


}
