using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : BasePanel 
{
    [SerializeField] private Button startGame;

    private ISceneManagement sceneManagement;

    private void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Services.Find(out sceneManagement);
        UIManager.Instance.ShowPanel(typeof(GameplayPanel));

        sceneManagement.NextScene();
    }

    public override void OverrideText()
    {

    }

}
