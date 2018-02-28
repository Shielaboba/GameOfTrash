using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System.Timers;
using UnityEngine.SceneManagement;
using System;

public class TrashDropScript : MonoBehaviour
{
    public GameObject[] obj;
    GameObject optionsPanel;
    GameObject replayPanel;
    GameObject btnPoint;
    Boolean flagDone;
    Text timer;
    List<TrashData> trash;
    PowerUpManager pu_manager;
    public float timeLeft, timeFinish;
    int selLevel, currLevel;

    private void Start()
    {
        selLevel = LevelManager.GetInstance().GetSelectLevel();
        currLevel = LevelManager.GetInstance().GetLevel();
        optionsPanel = GameObject.Find("optionsPanel");
        replayPanel = GameObject.Find("replayPanel");
        btnPoint = GameObject.Find("pointsbtn");
        timer = GameObject.Find("timer").GetComponent<Text>();
        timeLeft = 120.0f;
        flagDone = false;
        SelectLevelGame();        
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        //display
        if (timeLeft >= 0)
        {
            replayPanel.SetActive(false);
            
            timer.color = Color.red;
            if (flagDone.Equals(true))
            {        
                Time.timeScale = 0;
                timeFinish = timeLeft;
                
                if (Time.timeScale == 0 && timeFinish >= 100.0f)
                    PowerUpManager.CheckGivePoint = true;
               else
                    btnPoint.SetActive(false);
            }

            // }
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
      
        else
        {
            FailedLevel();
        }

        if (transform.childCount-4 == 0)
        {
            SuccessLevel();
        }
        else
        {
            optionsPanel.SetActive(false);
        }

        if(PlayerPrefs.GetInt("PlayerCurrentLives") == 0)
        {
            FailedLevel();
        }
    }

    void SuccessLevel()
    {
        flagDone = true;
        optionsPanel.SetActive(true);
        PowerUpManager.CheckGiveLife = true;
        Button btn = GameObject.Find("GoButton").GetComponent<Button>();

        btn.onClick.AddListener(delegate ()
        {
            if (selLevel == currLevel)
                ScoreScript.scorePoints = 0;            
        });
    }

    void FailedLevel()
    {
        replayPanel.SetActive(true);
        Button btn = GameObject.Find("RepBtn").GetComponent<Button>();

        btn.onClick.AddListener(delegate ()
        {
            Destroy(this);
            ScoreScript.scorePoints = 0;
            SceneManager.LoadScene("map");
        });
    }

    public void SelectLevelGame()
    {        
        if (!gameObject.name.Equals("TrashSegLevel" + selLevel))
        {                
            gameObject.SetActive(false);
        }
        else StartCoroutine("DeployTrash");
    }

    IEnumerator DeployTrash()
    {
        trash = TrashRandomManager.GetInstance().GetTrash();
        for (int i = 0; i < obj.Length; i++)
        {
            WWW www = new WWW(trash[i].TrashUrl);
            yield return www;

            if (www.isDone) obj[i].SetActive(true);
            obj[i].GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            obj[i].name = trash[i].TrashName;
            Instantiate(obj[i]);
        }
    }
}
