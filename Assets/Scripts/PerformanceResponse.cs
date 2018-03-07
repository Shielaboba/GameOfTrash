using System.Collections.Generic;
using System;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.timer;
using UnityEngine.SceneManagement;

public class PerformanceResponse : App42CallBack {

    PlayerData Data;
    private MapScript MapScriptInstance;

    private string exitTime;
    private string returnTime;

    int addedLife;
    
    double timeToDisplay;

    public void OnSuccess(object response)
    {        
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

        Data = JsonUtility.FromJson<PlayerData>(jsonDocList[0].GetJsonDoc());
        PlayerManager.GetInstance().SetPlayer(Data);
        PlayerPrefs.SetInt("PlayerCurrentScore", 0);
        
        int lifeLeft = PlayerManager.GetInstance().GetPlayer().PlayerLife; // get life in db for comparison
        double timeRemainWhenLeft = PlayerManager.GetInstance().GetPlayer().PlayerLifeTimer; 

        exitTime = PlayerManager.GetInstance().GetPlayer().PlayerExitTime;

        TimerService timerService = App42API.BuildTimerService();
        timerService.GetCurrentTime(new CurrentTimeResponse());

        Debug.Log(PlayerPrefs.GetString("CurrentTime"));
        returnTime = PlayerPrefs.GetString("CurrentTime");

        if (lifeLeft == 5)
        {
            PlayerPrefs.SetInt("PlayerCurrentLives", 5); // SET NUMBER OF PLAYING LIFE
            PlayerPrefs.SetInt("PlayerLifeTimer", 2400);
        }
        else
        {
            // Calculate timeToDisplay 
            // Calculate addedLife

            // To Calculate time : 
            // Subtract Return time with Exit time = timeElapsed 
            // if timeElapsed is less than or equal timeRemainingWhenLeft, Subtract timeRemainingWhenLeft - timeElapsed = timeToDisplay, PlayerLife remains the same
            // else if timeElapsed is Greater than timeRemainingWhenLeft, divide timeElapsed by 40 mins (2400 sec), answer is addedLife, remainder is timeToDisplay  

            TimeSpan duration = DateTime.Parse(returnTime).Subtract(DateTime.Parse(exitTime)); 
            double timeElapsed = duration.TotalSeconds;

            if (timeElapsed <= timeRemainWhenLeft)
            {
                timeToDisplay = timeRemainWhenLeft - timeElapsed; // get time directly 
            }
            else if (timeElapsed > timeRemainWhenLeft)
            {
                // right time is the time to be displayed in UI
                timeToDisplay = timeElapsed % 2400;              // mao ni ang bag.ong oras
                addedLife = Convert.ToInt32(timeElapsed) / 2400; // mao ni ang bag.ong score
            }

            if (lifeLeft + addedLife >= 5)
            {
                PlayerPrefs.SetInt("PlayerCurrentLives", 5); // SET NUMBER OF PLAYING LIFE
                PlayerPrefs.SetInt("PlayerLifeTimer", 2400); // SET TIMER BACK TO 2400 (40 mins)
            }
            else
            {
                PlayerPrefs.SetInt("PlayerCurrentLives", (lifeLeft + addedLife)); // SET NUMBER OF PLAYING LIFE
                PlayerPrefs.SetInt("PlayerLifeTimer", Convert.ToInt32(timeToDisplay)); 
            }
        }

        SceneManager.LoadScene("map");
        var lvlManager = LevelManager.GetInstance();
        lvlManager.SetLevel(Data.PlayerGameLvlNo);

    }

    public void OnException(Exception e)        
    {
        Debug.Log(e.Message);        
        App42Log.SetDebug(true);
    }
}
