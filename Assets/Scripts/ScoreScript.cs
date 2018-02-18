using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;

public class ScoreScript : MonoBehaviour
{
    public static int scorePoints = 0;
    Text score;
    String gameName = "GOT";
    String userName = Environment.UserName;
    int gameScore = ScoreScript.scorePoints;


    // Use this for initialization
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scorePoints;
    }

    public void SaveLeaderBoard()
    {
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        GameService gameService = App42API.BuildGameService();
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(gameName, userName, gameScore, new ScoreResponse());
    }
}
