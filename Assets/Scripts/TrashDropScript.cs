﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TrashDropScript : MonoBehaviour
{
    public GameObject[] obj;
    GameObject optionsPanel, replayPanel, btnPoint,panelModals, congratulationspanel;
    Boolean flagDone;
    Text timer;
    List<TrashData> trash;
    PowerUpManager pu_manager;
    public float timeLeft, timeFinish;
    int selLevel, currLevel;
    public bool stopTimer;
    Button btn;
   

    private void Start()
    {
        selLevel = LevelManager.GetInstance().GetSelectLevel();
        currLevel = LevelManager.GetInstance().GetLevel();
        optionsPanel = GameObject.Find("optionsPanel");
        btn = GameObject.Find("GoButton").GetComponent<Button>();
        congratulationspanel = GameObject.Find("congratulationsPanel");
        replayPanel = GameObject.Find("replayPanel");
        panelModals = GameObject.Find("PanelModals");
        btnPoint = GameObject.Find("givepupoints");
        timer = GameObject.Find("timer").GetComponent<Text>();
        timeLeft = 120.0f;
        flagDone = false;
        stopTimer = false;
        SelectLevelGame();
    }

    private void Update()
    {
        if (!StopTimer()) 
            timeLeft -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        if (timeLeft >= 0)
        {
            panelModals.SetActive(false);
            replayPanel.SetActive(false);
            congratulationspanel.SetActive(false);

            timer.color = Color.red;
            if (flagDone.Equals(true))
            {
                stopTimer = true;
                timeFinish = timeLeft;
                
                if (timeFinish >= 100.0f)
                    PowerUpManager.CheckGivePoint = true;//for point power up
               else
                    btnPoint.SetActive(false);
            }   

            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }      
        else
        {
            panelModals.SetActive(true);
            replayPanel.SetActive(true);
            congratulationspanel.SetActive(false);
        }

        if (transform.childCount-4 == 0)
        {
            SuccessLevel();
        }

        if(PlayerPrefs.GetInt("PlayerCurrentLives") == 0)
        {
            panelModals.SetActive(true);
            replayPanel.SetActive(true);
            congratulationspanel.SetActive(false);
        }
    }
    public bool StopTimer()
    {

        if (stopTimer)
            return true;

        return false;

    }
    void SuccessLevel()
    {
        if ((selLevel != 6) || (currLevel != 6))
        {
            flagDone = true;
            panelModals.SetActive(true);
            optionsPanel.SetActive(true);
            congratulationspanel.SetActive(false);
            PowerUpManager.CheckGiveLife = true;

        }
        else
        {
            flagDone = true;
            panelModals.SetActive(true);
            congratulationspanel.SetActive(true);
            PowerUpManager.CheckGiveLife = true;
        }
        

        btn.onClick.AddListener(delegate ()
        {
            ScoreScript.scorePoints = 0;            
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
