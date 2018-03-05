using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.email;
using System;
using UnityEngine;

public class Response : App42CallBack
{
    public void OnSuccess(object response)
    {

    }

    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
