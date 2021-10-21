using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour 
{
    [SerializeField] private Text currentLv;
    [SerializeField] private Text playerMoney;


    private void Start()
    {
        currentLv.text = "Level " + DataManager.Instance.CurrentLv.ToString();
        playerMoney.text = DataManager.Instance.Money.ToString();
    }

}
