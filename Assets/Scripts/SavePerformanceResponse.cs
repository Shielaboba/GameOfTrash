﻿using com.shephertz.app42.paas.sdk.csharp;
using System;
using UnityEngine.SceneManagement;

public class SavePerformanceResponse : App42CallBack
{
    public void OnSuccess(object response)
    {
        UnityEngine.PlayerPrefs.SetInt("PlayerCurrentScore", 0);
        SceneManager.LoadScene("map");
    }

    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
