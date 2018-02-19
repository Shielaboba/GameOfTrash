using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.shephertz.app42.paas.sdk.csharp.session;
using com.shephertz.app42.paas.sdk.csharp;

public class LoginSession : App42CallBack {
    public void OnSuccess(object response)
    {
        Session session = (Session)response;
        App42Log.Console("userName is" + session.GetUserName());
        App42Log.Console("userName is" + session.GetSessionId());
        App42Log.Console("userName is" + session.GetCreatedOn());
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
