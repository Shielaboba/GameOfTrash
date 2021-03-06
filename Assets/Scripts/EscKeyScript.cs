﻿using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.timer;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class EscKeyScript : MonoBehaviour {

    PlayerData player;

    GameObject procedure, diymain;

    // Use this for initialization
    void Start ()
    {
        player = PlayerManager.GetInstance().GetPlayer();
        procedure = GameObject.Find("procedure");
        diymain = GameObject.Find("diymain");
    }
	
	// Update is called once per frame
	void Update () {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("login_menu"))
        {
            if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene("main_menu");
            }
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("reg_menu"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("login_menu");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("leaderboard_display"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("map");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("trash_search"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("trash_menu");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("trash_menu"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("map");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("trivia_menu"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("trash_menu");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DIY"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (procedure.activeInHierarchy)
                {
                    diymain.SetActive(true);
                    procedure.SetActive(false);
                }
            }
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene("trash_menu");
    }

    public void ReturnMap()
    {
        SceneManager.LoadScene("map");
    }

    public void SegShow()
    {
        PlayerPrefs.SetInt("LifePUcount", PlayerManager.GetInstance().GetPlayer().PlayerPowerLife);
        PlayerPrefs.SetInt("PointPUcount", PlayerManager.GetInstance().GetPlayer().PlayerPowerScore);
        SceneManager.LoadScene("trash_seg");
    }

    public void ReturnDiyMain()
    {
        print("hey");
        diymain.SetActive(true);
    }

    public void GotoBook()
    {
        SceneManager.LoadScene("book");
    }

    public void ExitApp()
    {
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        player.PlayerLifeTimer = PlayerPrefs.GetInt("PlayerLifeTimer");
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");

        TimerService timerService = App42API.BuildTimerService();
        timerService.GetCurrentTime(new CurrentTimeResponse());

        Debug.Log(PlayerPrefs.GetString("CurrentTime"));
        player.PlayerExitTime = PlayerPrefs.GetString("CurrentTime"); //temp

        string data = JsonUtility.ToJson(player);

        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new SaveWhenLogoutExitResponse());

        Application.Quit();
    }

    public void Register()
    {
        SceneManager.LoadScene("reg_menu");
    }
}
