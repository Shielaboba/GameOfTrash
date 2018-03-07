using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp.timer;
using System;

public class LogoutScript : MonoBehaviour
{
    PlayerData player;

    string sessionID = LoginResponse.sessionID;

    void Start()
    {
        player = PlayerManager.GetInstance().GetPlayer();        
    }
    public void Logout()
    {
        
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        player.PlayerLifeTimer = PlayerPrefs.GetInt("PlayerLifeTimer"); // temp
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");

        TimerService timerService = App42API.BuildTimerService();
        timerService.GetCurrentTime(new CurrentTimeResponse());

        Debug.Log(PlayerPrefs.GetString("CurrentTime"));
        player.PlayerExitTime = PlayerPrefs.GetString("CurrentTime"); //temp
        
        string data = JsonUtility.ToJson(player);
            
        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new SaveWhenLogoutExitResponse());

        UserService userService = App42API.BuildUserService();
        userService.Logout(sessionID, new Response());

        SceneManager.LoadScene("login_menu");

    }
}
