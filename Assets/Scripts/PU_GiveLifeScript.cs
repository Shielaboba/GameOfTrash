using System;
using UnityEngine;
using UnityEngine.UI;

public class PU_GiveLifeScript : MonoBehaviour {
    public Text addedLife;
    LifeManager life_manager;
    PowerUpManager pu_manager;
    GameObject btnGiveLife;
    Boolean flagClick;
    int count;
  
	// Use this for initialization
	void Start () {
        addedLife = GameObject.Find("countLifeAdded").GetComponent<Text>();
        count = PlayerManager.GetInstance().GetPlayer().PlayerPowerLife;
        life_manager = FindObjectOfType<LifeManager>();
        btnGiveLife = GameObject.Find("lifebtn"); ;
        addedLife.text = count + "";
        flagClick = false;
    }
	
    public void OnCLickGiveLife()
    {
        flagClick = true;
        if (PowerUpManager.CheckGiveLife.Equals(true))
            count++;  

        addedLife.text = "" + count;

        if (flagClick.Equals(true))
            btnGiveLife.SetActive(false);
    }
    public void OnClick()
    {
        PowerUpManager.CheckDoublePoint = true;
        if (count!=0)
        {
            life_manager.GiveLife();
            count--;
            addedLife.text = count + "";
        }
      
        if (count <= 0)
        {
            count = 0;
            addedLife.text = "" + 0;
        }
    }
}
