using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    private float countingTime;

    LifeManager lifeManager;

    public Text clock;

    void Start()
    {
        clock = GetComponent<Text>();

        lifeManager = FindObjectOfType<LifeManager>();
        
        countingTime = PlayerPrefs.GetInt("PlayerLifeTimer");

    }

    void Update()
    {
        if (lifeManager.GetCurHealth() == 5)
            return;

        countingTime -= Time.deltaTime;
        int min = Mathf.FloorToInt(countingTime / 60F);
        int sec = Mathf.FloorToInt(countingTime - min * 60);

        if(countingTime <= 0)
        {
            //.. to do 

            lifeManager.GiveLife();

            ResetTime();
        }
        
        PlayerPrefs.SetInt("PlayerLifeTimer", Convert.ToInt32(countingTime));

        clock.text = string.Format("{0:0}:{1:00}", min, sec);
    }

    public void ResetTime()
    {
        countingTime = 20;
    }
}
