using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PU_PointsScript : MonoBehaviour {

    PowerUpManager pu_manager;
    public Text addedPoint;
    int countGivePoint;
    Boolean flagClick;
    GameObject btnPoint;

    void Start()
    {
        addedPoint = GameObject.Find("countPointAdded").GetComponent<Text>();
        btnPoint = GameObject.Find("pointsbtn");
        countGivePoint = PlayerManager.GetInstance().GetPlayer().PlayerPowerScore;
        addedPoint.text = countGivePoint + "";
        flagClick = false;
    }
    
	public void OnclickPoints()
    {
        flagClick = true;
      
        if (PowerUpManager.CheckGivePoint.Equals(true))
            countGivePoint++;

       addedPoint.text = "" + countGivePoint;

        if (flagClick.Equals(true))
            btnPoint.SetActive(false);
       
    }

    public void OnClickUsePU()
    {
        PowerUpManager.CheckDoublePoint = true;
        if (countGivePoint != 0)
        {
            Debug.Log("ACCESS");
            countGivePoint--;
            addedPoint.text = countGivePoint+"";
        }

        if (countGivePoint <= 0)
        {
            countGivePoint = 0;
            Debug.Log("No more power up");
            addedPoint.text = "" + 0;
        }
    }
}
