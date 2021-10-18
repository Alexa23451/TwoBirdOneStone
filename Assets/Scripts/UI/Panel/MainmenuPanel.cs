using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour 
{
    [SerializeField] private Text currentLv;


    private void Start()
    {
        currentLv.text = "Level " + DataManager.Instance.CurrentLv.ToString();
    }

}
