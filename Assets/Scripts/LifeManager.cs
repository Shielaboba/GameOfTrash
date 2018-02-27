using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.timer;
using System;

public class LifeManager : MonoBehaviour
{

    public Sprite[] HeartSprites;
    public Image HeartsUI; 

    private int currentHealth; 

    private float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue = 1200)
    {
        currCountdownValue = countdownValue;                
        
        while (currentHealth != 5)
        {
            if (currCountdownValue == 0)
            {
                GiveLife(); 
                currCountdownValue = 1200;
            }            
                    
            yield return new WaitForSeconds(1.0f); 
            currCountdownValue--;

            if (currentHealth == 5)
                Debug.Log("Life full");
        }
    }

    void Start ()
    {
         
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives"); 

    }
    void Update()
    {
        HeartsUI.sprite = HeartSprites[currentHealth];

    }
    public void TakeLife()
    {
        currentHealth--;
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth); 
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