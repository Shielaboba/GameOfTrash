using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using System;

public class SaveWhenLogoutExitResponse : App42CallBack {

    public void OnSuccess(object response)
    {
        //PlayerPrefs.SetInt("PlayerCurrentScore", 0);        
    }

    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
