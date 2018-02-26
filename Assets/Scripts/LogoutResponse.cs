using com.shephertz.app42.paas.sdk.csharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogoutResponse : App42CallBack
{
    public void OnSuccess(object response)
    {

    }
    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }    
}
