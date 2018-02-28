using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;

public class PerformanceResponse : App42CallBack {

    PlayerData Data;
    private MapScript MapScriptInstance;
    
    public void OnSuccess(object response)
    {        
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

        Data = JsonUtility.FromJson<PlayerData>(jsonDocList[0].GetJsonDoc());
        PlayerManager.GetInstance().SetPlayer(Data);
        PlayerPrefs.SetInt("PlayerCurrentScore", 0);
        PlayerPrefs.SetInt("PlayerCurrentLives", PlayerManager.GetInstance().GetPlayer().PlayerLife);// .. SET NUMBER OF PLAYING LIFE
        PlayerPrefs.SetInt("PlayerLifeTimer", 2400);
        SceneManager.LoadScene("map");
        var lvlManager = LevelManager.GetInstance();
        lvlManager.SetLevel(Data.PlayerGameLvlNo);

    }

    public void OnException(Exception e)        
    {
        Debug.Log(e.Message);
        App42Log.Console("Exception : " + e);
        App42Log.SetDebug(true);
    }
}
