using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    public const string GAME_DATA_PATH = "GameData";

    private GameData _gameData;

    private JsonManager _jsonManager;

    public override void Init()
    {
        _jsonManager = new JsonManager();
        _gameData = _jsonManager.ReadDataFromFile<GameData>(GAME_DATA_PATH);

        IAPManager.Instance.OnRemoveAds += OnRemoveAds;
    }

    private void OnRemoveAds()
    {
        Debug.Log("ON PURCHASE REMOVE ADS");
        RemoveAdsOn = true;
    }

    public void SaveGame() => _jsonManager.WriteDataToFile(_gameData, GAME_DATA_PATH);

    public int Money { get => _gameData.playerMoney; set => _gameData.playerMoney = value;}
    public int CurrentLv { get => _gameData.currentLv; set => _gameData.currentLv = value;}
    public int UnlockLv { get => _gameData.unlockLv; set => _gameData.unlockLv = value; }
    public bool VibrateOn { get => _gameData.vibrateOn; set => _gameData.vibrateOn = value; }
    public bool SoundOn { get => _gameData.soundOn; set => _gameData.soundOn = value; }
    public bool RemoveAdsOn { get => _gameData.removeAds; set => _gameData.removeAds = value; }

    [ContextMenu("SEE DATA")]
    public void WatchData()
    {
        Debug.Log(_gameData.removeAds);
    }

    private void OnDestroy()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveGame();
    }
}