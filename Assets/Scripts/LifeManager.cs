using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.timer;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartsUI; 
    private int currentHealth;
    GameObject timeManager;    

    void Start ()
    {
        timeManager = GameObject.Find("Time System");
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

        timeManager.SetActive(true); // if na kwaan ang life, start ang timer.
        
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