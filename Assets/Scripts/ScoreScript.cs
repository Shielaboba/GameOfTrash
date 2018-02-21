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
    Text score;
   
    // Use this for initialization
    void Start()
    {
        score = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scorePoints;
    }
    

}
