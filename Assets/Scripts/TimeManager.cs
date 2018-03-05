using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    private float countingTime;
    LifeManager lifeManager;
    public Text clock;
    GameObject timeManager;

    void Start()
    {
        timeManager = GameObject.Find("Time System");                        
        countingTime = PlayerPrefs.GetInt("PlayerLifeTimer");
        lifeManager = FindObjectOfType<LifeManager>();
        clock = GetComponent<Text>();
    }

    void Update()
    {
        if (lifeManager.GetCurHealth() == 5)
        {
            PlayerPrefs.SetInt("PlayerLifeTimer", 2400);
            timeManager.SetActive(false);
            return;
        }

        else
        {
            timeManager.SetActive(true);

            countingTime -= Time.deltaTime;

            int min = Mathf.FloorToInt(countingTime / 60F);
            int sec = Mathf.FloorToInt(countingTime - min * 60);

            if (countingTime <= 0)
            {
                lifeManager.GiveLife();
                ResetTime();
            }

            PlayerPrefs.SetInt("PlayerLifeTimer", Convert.ToInt32(countingTime));
            clock.text = string.Format("{0:0}:{1:00}", min, sec);
        }
    }

    public void ResetTime()
    {
        countingTime = 2400;
    }
}