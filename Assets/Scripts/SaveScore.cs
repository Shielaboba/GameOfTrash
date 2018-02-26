using UnityEngine;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    PlayerData player;
    Constant cons;
    int currLevel, selLevel;
    private void Start()
    {
        player = PlayerManager.GetInstance().GetPlayer();
        cons = new Constant();
    }

    public void SaveLeaderBoard()
    {
        String userName = player.PlayerName; //PlayerPrefs.GetString("username");
        //int gameScore = ScoreScript.scorePoints;
        player.PlayerScoreMade = ScoreScript.scorePoints;
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(cons.gameName, userName, player.PlayerScoreMade, new ScoreResponse());

        App42Log.SetDebug(true);
    }

    public void SavePerfLevel()
    {
        selLevel = LevelManager.GetInstance().GetSelectLevel();
        currLevel = LevelManager.GetInstance().GetLevel();
        if (selLevel == currLevel)
            LevelManager.GetInstance().SetLevel(currLevel + 1);

        player.PlayerGameLvlNo = LevelManager.GetInstance().GetLevel();
        player.PlayerScoreMade = 0;
        switch (selLevel)
        {
            case 1:
                player.PlayerScoreLevel[0] += ScoreScript.scorePoints;
                break;
            case 2:
                player.PlayerScoreLevel[1] += ScoreScript.scorePoints;
                break;
            case 3:
                player.PlayerScoreLevel[2] += ScoreScript.scorePoints;
                break;
            case 4:
                player.PlayerScoreLevel[3] += ScoreScript.scorePoints;
                break;
            case 5:
                player.PlayerScoreLevel[4] += ScoreScript.scorePoints;
                break;
            case 6:
                player.PlayerScoreLevel[5] += ScoreScript.scorePoints;
                break;
        }

        for (int i = 0; i < player.PlayerScoreLevel.Count; i++)
        {
            player.PlayerScoreMade += player.PlayerScoreLevel[i];
        }
        PlayerPrefs.SetInt("PlayerCurrentScore", 0);
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");
        player.PlayerPowerLife = int.Parse(GameObject.Find("countLifeAdded").GetComponent<Text>().text);
        player.PlayerPowerScore = int.Parse(GameObject.Find("countPointAdded").GetComponent<Text>().text);
        print("Name: " + player.PlayerName +
            "Level: " + player.PlayerGameLvlNo +
            "Life: " + player.PlayerLife +
            "PU Score: " + player.PlayerPowerScore +
            "PU Life: " + player.PlayerPowerLife +
            "Level1: " + player.PlayerScoreLevel[0] +
            "Scores: " + player.PlayerScoreMade);

        string data = JsonUtility.ToJson(player);
        print(data);
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(cons.gameName, player.PlayerName, player.PlayerScoreMade, new ScoreResponse());

        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new SavePerformanceResponse());

        
    }
}