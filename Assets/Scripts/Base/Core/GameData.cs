using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SettingsSingleton<GameData>
{
    public int playerMoney;
    public int currentLv;
    public int unlockLv;

    public GameData()
    {
        playerMoney = 0;
        currentLv = 1;
        unlockLv = 1;
    }
}
