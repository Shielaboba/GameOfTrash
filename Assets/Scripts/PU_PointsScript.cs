using System;
using UnityEngine;
using UnityEngine.UI;

public class PU_PointsScript : MonoBehaviour
{

    PowerUpManager pu_manager;
    public Text addedPoint;
    int countGivePoint;
    int updatedPointValue;
    Boolean flagClickPU, flagClickUsePU;
    Button doublePointBtn;
    GameObject btnPoint;

    void Start()
    {
        addedPoint = GameObject.Find("countPointAdded").GetComponent<Text>();
        btnPoint = GameObject.Find("givepupoints");
        doublePointBtn = GameObject.Find("doublePoints").GetComponent<Button>();
        countGivePoint = PlayerPrefs.GetInt("PointPUcount");
        addedPoint.text = countGivePoint + "";
        flagClickPU = false;
        flagClickUsePU = false;
    }
    //use point powerup
    public void OnClickUsePU()
    {
        flagClickUsePU = true;

        //double score
        PowerUpManager.CheckDoublePoint = true;

        if (countGivePoint != 0)
        {
            countGivePoint--;
            PlayerPrefs.SetInt("PointPUcount", countGivePoint);
            addedPoint.text = "" + PlayerPrefs.GetInt("PointPUcount"); ;
        }

        if (countGivePoint <= 0)
        {
            countGivePoint = 0;
            addedPoint.text = "" + 0;
        }

        if (flagClickUsePU.Equals(true))
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

        if (flagClickPU)
            btnPoint.SetActive(false);

    }


}
