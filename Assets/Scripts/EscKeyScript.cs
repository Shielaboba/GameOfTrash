using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class EscKeyScript : MonoBehaviour {

    PlayerData player;
    LifeManager life_manager;
    GameObject procedure, diymain;

    // Use this for initialization
    void Start ()
    {
        player = PlayerManager.GetInstance().GetPlayer();
        life_manager = FindObjectOfType<LifeManager>();
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
        procedure.SetActive(false);
    }

    public void GotoBook()
    {
        SceneManager.LoadScene("book");
    }

    public void ExitApp()
    {
        Constant cons = new Constant();

        player.PlayerLifeTimer = PlayerPrefs.GetInt("PlayerLifeTimer");
        player.PlayerLife = PlayerPrefs.GetInt("PlayerCurrentLives");

        string data = JsonUtility.ToJson(player);

        App42API.Initialize(cons.apiKey, cons.secretKey);

        StorageService storageService = App42API.BuildStorageService();
        storageService.UpdateDocumentByKeyValue(cons.dbName, "PerformanceFile", "PlayerName", player.PlayerName, data, new Response());

        Application.Quit();
    }

    public void Register()
    {
        SceneManager.LoadScene("reg_menu");
    }

    public void ExitTrashMenu()
    {
        life_manager.TakeLife();
        SceneManager.LoadScene("map");
    }

    public void ReplayClickBtn()
    {
        ScoreScript.scorePoints = 0;
        SceneManager.LoadScene("map");
    }
}
