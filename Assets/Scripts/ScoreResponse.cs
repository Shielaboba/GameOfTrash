using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;
using UnityEngine.UI;

public class ScoreResponse : App42CallBack
{
    public void OnSuccess(object response)
    {
        Game game = (Game)response;

        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            PlayerPrefs.SetInt("PlayerCurrentScore", Convert.ToInt32(game.GetScoreList()[i].GetValue()));
        }
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
