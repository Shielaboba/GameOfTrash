using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.timer;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;

public class SavePlayer : MonoBehaviour
{
    PlayerData player;
    Constant cons;
    int currLevel, selLevel;
    int totalScore;

    private void Start()
    {
        player = PlayerManager.GetInstance().GetPlayer();
        cons = new Constant();
    }

    public void SavePerfLevel()
    {
        selLevel = LevelManager.GetInstance().GetSelectLevel();
        currLevel = LevelManager.GetInstance().GetLevel();
        if (selLevel == currLevel)
            LevelManager.GetInstance().SetLevel(currLevel + 1);

        player.PlayerGameLvlNo = LevelManager.GetInstance().GetLevel();
        totalScore = 0;

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
            totalScore += player.PlayerScoreLevel[i];
        }

        PlayerPrefs.SetInt("PlayerCurrentScore", 0);
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");
        player.PlayerLifeTimer = PlayerPrefs.GetInt("PlayerLifeTimer");

        TimerService timerService = App42API.BuildTimerService();
        timerService.GetCurrentTime(new CurrentTimeResponse());

        Debug.Log(PlayerPrefs.GetString("CurrentTime"));
        player.PlayerExitTime = PlayerPrefs.GetString("CurrentTime");
        
        player.PlayerPowerLife = int.Parse(GameObject.Find("countLifeAdded").GetComponent<Text>().text);
        player.PlayerPowerScore = int.Parse(GameObject.Find("countPointAdded").GetComponent<Text>().text);
        

        string data = JsonUtility.ToJson(player);
        print(data);
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore(cons.gameName, player.PlayerName, totalScore, new Response());

        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new SavePerformanceResponse());

        
    }
}