using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;
using UnityEngine.UI;

public class LeaderboardResponse : App42CallBack
{
   
    public Text userNameText, userScoreText;
    public Text userNameText2, userScoreText2;
    public Text userNameText3, userScoreText3;
    public Text userNameText4, userScoreText4;
    public Text userNameText5, userScoreText5;
    public Text userNameText6, userScoreText6;
    public Text userNameText7, userScoreText7;
    public Text userNameText8, userScoreText8;
    public Text userNameText9, userScoreText9;
    public Text userNameText10, userScoreText10;

    void Init()
    {
        userNameText = GameObject.Find("userNameText").GetComponent<Text>();
        userScoreText = GameObject.Find("userScoreText").GetComponent<Text>();
        userNameText2 = GameObject.Find("userNameText2").GetComponent<Text>();
        userScoreText2 = GameObject.Find("userScoreText2").GetComponent<Text>();
        userNameText3 = GameObject.Find("userNameText3").GetComponent<Text>();
        userScoreText3 = GameObject.Find("userScoreText3").GetComponent<Text>();
        userNameText4 = GameObject.Find("userNameText4").GetComponent<Text>();
        userScoreText4 = GameObject.Find("userScoreText4").GetComponent<Text>();
        userNameText5 = GameObject.Find("userNameText5").GetComponent<Text>();
        userScoreText5 = GameObject.Find("userScoreText5").GetComponent<Text>();
        userNameText6 = GameObject.Find("userNameText6").GetComponent<Text>();
        userScoreText6 = GameObject.Find("userScoreText6").GetComponent<Text>();
        userNameText7 = GameObject.Find("userNameText7").GetComponent<Text>();
        userScoreText7 = GameObject.Find("userScoreText7").GetComponent<Text>();
        userNameText8 = GameObject.Find("userNameText8").GetComponent<Text>();
        userScoreText8 = GameObject.Find("userScoreText8").GetComponent<Text>();
        userNameText9 = GameObject.Find("userNameText9").GetComponent<Text>();
        userScoreText9 = GameObject.Find("userScoreText9").GetComponent<Text>();
        userNameText10 = GameObject.Find("userNameText10").GetComponent<Text>();
        userScoreText10 = GameObject.Find("userScoreText10").GetComponent<Text>();
    }
    public void OnSuccess(object response)
    {
        Init();

        Game game = (Game)response;
        App42Log.Console("gameName is " + game.GetName());

        //IList<Game.JSONDocument> jsonDocList = game.GetJsonDocList();

        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());
            App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());
            App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());

            if (i == 0)
            {
                userNameText.text = game.GetScoreList()[i].GetUserName();
                userScoreText.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 1)
            {
                userNameText2.text = game.GetScoreList()[i].GetUserName();
                userScoreText2.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 2)
            {
                userNameText3.text = game.GetScoreList()[i].GetUserName();
                userScoreText3.text = game.GetScoreList()[i].GetValue().ToString() + " points";

            }
            else if (i == 3)
            {
                userNameText4.text = game.GetScoreList()[i].GetUserName();
                userScoreText4.text = game.GetScoreList()[i].GetValue().ToString() + " points";

            }
            else if (i == 4)
            {
                userNameText5.text = game.GetScoreList()[i].GetUserName();
                userScoreText5.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 5)
            {
                userNameText6.text = game.GetScoreList()[i].GetUserName();
                userScoreText6.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 6)
            {
                userNameText7.text = game.GetScoreList()[i].GetUserName();
                userScoreText7.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 7)
            {
                userNameText8.text = game.GetScoreList()[i].GetUserName();
                userScoreText8.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 8)
            {
                userNameText9.text = game.GetScoreList()[i].GetUserName();
                userScoreText9.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }
            else if (i == 9)
            {
                userNameText10.text = game.GetScoreList()[i].GetUserName();
                userScoreText10.text = game.GetScoreList()[i].GetValue().ToString() + " points";
            }

        }

        App42Log.SetDebug(true);
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
        App42Log.SetDebug(true);
    }


}
