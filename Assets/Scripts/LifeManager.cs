﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartsUI; 
    private int currentHealth;
    GameObject timeManager;

    TimerService timerService = App42API.BuildTimerService();

    void Start ()
    {
        timeManager = GameObject.Find("Time System");
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives");        
    }

    void Update()
    {
        HeartsUI.sprite = HeartSprites[currentHealth];

        //TimerService timerService = App42API.BuildTimerService();
        //timerService.GetCurrentTime(new CurrentTimeResponse());
    }
    IEnumerator DoCheck()
    {
        for ( ; ; )
        {
            timerService.GetCurrentTime(new CurrentTimeResponse());

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void TakeLife()
    {
        currentHealth--;
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth);

        timeManager.SetActive(true); // if na kwaan ang life, start ang timer.

        // temp code

        //TimerService timerService = App42API.BuildTimerService();
        timerService.StartTimer("GiveLifeTimer", "summer", new TimerResponse());
        //StartCoroutine(DoCheck());
        //timerService.GetCurrentTime(new CurrentTimeResponse());
        
        //
    }

    public void GiveLife()
    {
        currentHealth++;
        if (currentHealth > 5)
        {
            currentHealth = 5;
        }
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth); 
    }

    public void SetCurHealth (int curHealth)
    {
        this.currentHealth = curHealth;
    }

    public int GetCurHealth()
    {
        return currentHealth;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth);
        SceneManager.LoadScene("map"); 
    }
}