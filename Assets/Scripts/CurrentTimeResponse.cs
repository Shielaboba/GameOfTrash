using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.timer;
using System;

public class CurrentTimeResponse : App42CallBack
{
    
    public void OnSuccess(object response)
    {
        Timer timer = (Timer)response;
        
        PlayerPrefs.SetString("CurrentTime", timer.GetCurrentTime().ToString());
    }

    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }    
}
