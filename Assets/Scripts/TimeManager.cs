using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {

    private float countingTime;

    LifeManager lifeManager;

    void Start()
    {

        lifeManager = FindObjectOfType<LifeManager>();
        
        countingTime = PlayerPrefs.GetInt("PlayerLifeTimer");

    }

    void Update()
    {
        if (lifeManager.GetCurHealth() == 5)
            return;

        countingTime -= Time.deltaTime;

        if(countingTime <= 0)
        {
            //.. to do 

            lifeManager.GiveLife();

            ResetTime();
        }
        
        PlayerPrefs.SetInt("PlayerLifeTimer", Convert.ToInt32(countingTime));
        Debug.Log(Mathf.Round(countingTime));
    }

    public void ResetTime()
    {
        countingTime = 20;
    }
}
