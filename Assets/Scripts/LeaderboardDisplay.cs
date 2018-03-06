using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;

public class LeaderboardDisplay : MonoBehaviour {    

    public void DisplayLeaderboard()
    {
        SceneManager.LoadScene("leaderboard_display");
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetTopNRankers(cons.gameName, 10, new LeaderboardResponse());
    }
}

internal class LeaderboardResponse : App42CallBack
{

    public void OnSuccess(object response)
    {
        Game game = (Game)response;

        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            GameObject.Find("userNameText" + (i + 1)).GetComponent<Text>().text = game.GetScoreList()[i].GetUserName();
            GameObject.Find("userScoreText" + (i + 1)).GetComponent<Text>().text = game.GetScoreList()[i].GetValue().ToString() + " points";
        }

        App42Log.SetDebug(true);
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
        App42Log.SetDebug(true);
    }
}
