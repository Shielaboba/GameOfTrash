using System;
using System.Collections;
using System.Collections.Generic;
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
        life_manager = FindObjectOfType<LifeManager>();
        btnGiveLife = GameObject.Find("lifebtn"); ;
        flagClick = false;
        count = PlayerManager.GetInstance().GetPlayer().PlayerPowerLife;
        addedLife.text = count + "";
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
            Debug.Log("No more power up");
            addedLife.text = "" + 0;
        }
        

    }
}
