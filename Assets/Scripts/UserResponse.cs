using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;

public class UserResponse : App42CallBack
{   
	Text errorMessage;

    void start()
    {
		errorMessage = GameObject.Find("warning").GetComponent<Text> ();
    }

    public void OnSuccess(object user)
    {
        try
        {
            User userObj = (User)user;
        }
        catch (App42Exception e)
        {
			errorMessage.text = e.ToString();
        }
    }

    public void OnException(Exception e)
    {
		App42Exception exception = (App42Exception)e;
		int appErrorCode = exception.GetAppErrorCode ();
		int httpErrorCode = exception.GetHttpErrorCode ();

		if (appErrorCode == 2002)
			errorMessage.text = exception.GetMessage ();
    }
		
}