using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class SaveScore : MonoBehaviour {

    public void SaveLeaderBoard()
    {
        String userName = PlayerPrefs.GetString("username");
        int gameScore = ScoreScript.scorePoints;

        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(cons.gameName, userName, gameScore, new ScoreResponse());

        App42Log.SetDebug(true);
    }
    public void savePerfLevel()
    {
        String userName = PlayerPrefs.GetString("username");
        int gameScore = ScoreScript.scorePoints;
        int level = LevelManager.GetInstance().GetLevel();
        String dbName = "GOTDB";
        String collectionName = "PerformanceFile";
        String perfJSON = "{\"PlayerGameLvlNo\":\"" + level + "\",\"PlayerScoreMade\":" + gameScore + ",\"PlayerName\":\"" + userName + "\"}";
        StorageService storageService = App42API.BuildStorageService();
        storageService.InsertJSONDocument(dbName, collectionName, perfJSON, new SavePerformanceResponse());
    }
}
