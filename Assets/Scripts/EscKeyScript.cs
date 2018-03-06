using UnityEngine;
using UnityEngine.SceneManagement;

public class EscKeyScript : MonoBehaviour {

    GameObject procedure, diymain, ExitOpt;
    LifeManager life_manager;
 
    // Use this for initialization
    void Start () {
        procedure = GameObject.Find("procedure");
        diymain = GameObject.Find("diymain");
        life_manager = FindObjectOfType<LifeManager>();
        ExitOpt = GameObject.Find("exitOption");
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
        life_manager.TakeLife();
    }

    public void SegShow()
    {
        
        PlayerPrefs.SetInt("LifePUcount", PlayerManager.GetInstance().GetPlayer().PlayerPowerLife);
        PlayerPrefs.SetInt("PointPUcount", PlayerManager.GetInstance().GetPlayer().PlayerPowerScore);
        SceneManager.LoadScene("trash_seg");
    }

    public void ReturnDiyMain()
    {
       
        diymain.SetActive(true);
        procedure.SetActive(false);
    }

    public void GotoBook()
    {
        SceneManager.LoadScene("book");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void Register()
    {
        SceneManager.LoadScene("reg_menu");
    }
}
