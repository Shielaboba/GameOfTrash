using System;
using UnityEngine;
using UnityEngine.UI;

public class PU_PointsScript : MonoBehaviour {

    public Text addedPoint;
    int countGivePoint;
    int updatedPointValue;
    Boolean flagClickPU, flagClickUsePU;
    Button doublePointBtn;
    GameObject btnPoint;

    void Start()
    {
        addedPoint = GameObject.Find("countPointAdded").GetComponent<Text>();
        btnPoint = GameObject.Find("pointsbtn");
        doublePointBtn = GameObject.Find("doublePoints").GetComponent<Button>();
        countGivePoint = PlayerPrefs.GetInt("PointPUcount");
        addedPoint.text = countGivePoint + "";
        flagClickPU = false;
        flagClickUsePU = false;
    }
    
	public void OnclickPoints()
    {
        flagClick = true;
      
        if (PowerUpManager.CheckGivePoint.Equals(true))
            countGivePoint++;

       addedPoint.text = "" + (countGivePoint- PlayerManager.GetInstance().GetPlayer().PlayerPowerScore);

        if (flagClick.Equals(true))
            btnPoint.SetActive(false);
       
    }

    public void OnClickUsePU()
    {
        flagClickUsePU = true;
        PowerUpManager.CheckDoublePoint = true;
        if (countGivePoint != 0)
        {
            countGivePoint--;
            PlayerPrefs.SetInt("PointPUcount", countGivePoint);
            addedPoint.text =  "" + PlayerPrefs.GetInt("PointPUcount"); ;
        }

        if (countGivePoint <= 0)
        {
            countGivePoint = 0;
            addedPoint.text = "" + 0;
        }

        if(flagClickUsePU.Equals(true))
        {
            doublePointBtn.enabled = false;
        }
    }
    //give point powerup
    public void OnclickGivePoints()
    {
        flagClickPU = true;

        if (PowerUpManager.CheckGivePoint.Equals(true))
        {
            updatedPointValue = PlayerPrefs.GetInt("PointPUcount");
            updatedPointValue++;
            PlayerPrefs.SetInt("PointPUcount", updatedPointValue);
        }
        addedPoint.text = "" + PlayerPrefs.GetInt("PointPUcount"); 

        if (flagClickPU.Equals(true))
            btnPoint.SetActive(false);
       
    }

  
}
