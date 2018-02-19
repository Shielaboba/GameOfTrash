using com.shephertz.app42.paas.sdk.csharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogoutResponse : App42CallBack
{
    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }

    public void OnSuccess(object response)
    {
        App42Response app42response = (App42Response)response;
        System.Diagnostics.Debug.WriteLine("response is " + app42response);
        throw new NotImplementedException();
    }
}
