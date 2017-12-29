using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;

public class PerformanceResponse : App42CallBack {

    public Data Data;
    private MapScript MapScriptInstance;
    
    public void OnSuccess(object response)
    {        
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

        Data = new Data();
        Data = JsonUtility.FromJson<Data>(jsonDocList[0].GetJsonDoc()); 
        
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
