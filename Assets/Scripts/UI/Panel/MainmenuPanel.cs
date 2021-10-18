using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour 
{
    [SerializeField] private Button startGame;

    private ISceneManagement sceneManagement;

    private void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        Services.Find(out sceneManagement);
        sceneManagement.NextScene();
    }

}
