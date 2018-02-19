using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public class Sample : MonoBehaviour {

    public void SaveLeaderBoard()
    {
        Constant cons = new Constant();
        String userName = "Tom";
        int gameScore = ScoreScript.scorePoints;

        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(cons.gameName, userName, gameScore, new ScoreResponse());

        App42Log.SetDebug(true);
    }
}
