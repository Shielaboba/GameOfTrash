using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour {

    public Sprite[] HeartSprites; // .. array of heart images (5,4,3,2,1,0)
    public Image HeartsUI; // .. image displayed in the user interface HUD

    private int currentHealth; // .. player remaining heart count

    private float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 20) // .. time needed to give life
    {
        currCountdownValue = countdownValue;        

        while (currentHealth != 5) // .. while current hearts is not max
        {
            if (currCountdownValue == 0)
            {
                GiveLife(); // .. increase hearts when timer reaches 0
                currCountdownValue = 20; // .. reset time
            }
            
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f); // .. decrement timer every 1 second
            currCountdownValue--;

            if (currentHealth == 5)
                Debug.Log("Life full");
        }
    }

    //Use this for initialization
    void Start ()
    {
        StartCoroutine(StartCountdown()); // .. start health timer
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives"); // .. player heart count upon logging in (see UserResponse.cs line 38)
    }
    void Update()
    {
        HeartsUI.sprite = HeartSprites[currentHealth]; // .. update heart count depending on currentHealth
    }
    public void TakeLife()
    {
        currentHealth--;
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth); // .. update player hearts
        StartCoroutine(StartCountdown()); // .. if life taken away, start countdown again
    }
    public void GiveLife()
    {
     
            currentHealth++;
        if (currentHealth > 5)
        {
            currentHealth = 5;
        }
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth); // .. update player hearts

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

   /* public void AddLifeByTime()
    {
        if(cu)
        {

        }
    }*/
}
