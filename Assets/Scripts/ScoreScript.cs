using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public class ScoreScript : MonoBehaviour
{
    public static int scorePoints = 0;
    Text scoreText;
   
    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        scorePoints = PlayerPrefs.GetInt("PlayerCurrentScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (scorePoints < 0)
            scorePoints = 0;

        scoreText.text = "Score: " + scorePoints;
    }
        
    public static void AddPoints (int pointsToAdd)
    {
        scorePoints += pointsToAdd;
        PlayerPrefs.SetInt("PlayerCurrentScore", scorePoints);
    } 

}


