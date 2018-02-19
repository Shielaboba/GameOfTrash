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
        App42Log.Console("gameName is " + game.GetName());

        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());
            App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());
            App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());
        }
        App42Log.SetDebug(true);
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
