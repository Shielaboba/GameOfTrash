using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp.storage;
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

        player.PlayerLifeTimer = PlayerPrefs.GetInt("PlayerLifeTimer");
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");

        string data = JsonUtility.ToJson(player);

        App42API.Initialize(cons.apiKey, cons.secretKey);

        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new SaveWhenLogoutExitResponse());

        UserService userService = App42API.BuildUserService();
        userService.Logout(sessionID, new Response());

        SceneManager.LoadScene("login_menu");
    }
}
