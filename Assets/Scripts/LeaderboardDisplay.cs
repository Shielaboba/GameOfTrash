using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public class LeaderboardDisplay : MonoBehaviour {

    public void Action_GotoPage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetTopRankings(cons.gameName, new LeaderboardResponse());

        int max = 10;
        scoreBoardService.GetTopNRankers(cons.gameName, max, new LeaderboardResponse());
    }
}
