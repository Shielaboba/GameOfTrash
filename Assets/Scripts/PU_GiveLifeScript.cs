using System;
using UnityEngine;
using UnityEngine.UI;

public class PU_GiveLifeScript : MonoBehaviour {
    public Text addedLife;
    LifeManager life_manager;
    PowerUpManager pu_manager;
    GameObject btnGiveLife;
    Button addLifebtn;
    Boolean flagClick;
    int countLife;
    int x;//updated count value for using power up
	// Use this for initialization
	void Start () {
        addedLife = GameObject.Find("countLifeAdded").GetComponent<Text>();

       
        countLife = PlayerPrefs.GetInt("LifePUcount");// iya gikuha pila ang value nga na set did2 sa button clicked.
        life_manager = FindObjectOfType<LifeManager>();
        btnGiveLife = GameObject.Find("lifebtn");
        addLifebtn = GameObject.Find("addLife").GetComponent<Button>();
        addedLife.text = countLife + "";
        flagClick = false;
        Debug.Log(life_manager.GetCurHealth());
    }
    void Update()
    {
        flagClick = true;
        if (PowerUpManager.CheckGiveLife.Equals(true))
            countLife++;  
        addedLife.text = "" + (countLife - PlayerManager.GetInstance().GetPlayer().PlayerPowerLife);

        if (life_manager.GetCurHealth() == 5)
        {
            addLifebtn.enabled = false;
           
        }
        else
            addLifebtn.enabled = true;

    }

    //use powerup
    public void OnClickUsePU()
    {
      
        PowerUpManager.CheckDoublePoint = true;
        if (countLife != 0 )
        {
                life_manager.GiveLife();
                countLife--;
                PlayerPrefs.SetInt("LifePUcount", countLife);

                addedLife.text = "" + PlayerPrefs.GetInt("LifePUcount");
            
        }

        if (countLife <= 0)
        {
            countLife = 0;
            addedLife.text = "" + 0;
        }
    }
    //give powerup
    public void OnCLickGiveLife()
    {
        flagClick = true;
        if (PowerUpManager.CheckGiveLife.Equals(true))
        {
            //count++;
            x = PlayerPrefs.GetInt("LifePUcount");
            x++;
            PlayerPrefs.SetInt("LifePUcount", x);
            Debug.Log(x);

        }


        addedLife.text = "" + PlayerPrefs.GetInt("LifePUcount");

        if (flagClick.Equals(true))
            btnGiveLife.SetActive(false);
    }
   
}
