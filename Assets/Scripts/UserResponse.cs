using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
using Newtonsoft.Json.Linq;
public class UserResponse : App42CallBack
{   
	Text errorMessage;
	String jsonTxt;

    void start()
    {
		errorMessage = GameObject.Find("warning").GetComponent<Text> ();
    }

    public void OnSuccess(object user)
    {
        try
        {
			
        }
        catch (App42Exception e)
        {
			
        }
    }

    public void OnException(Exception e)
    {		
		
		App42Exception exception = (App42Exception) e ;
		Debug.Log (exception.GetAppErrorCode ());
		//int appErrorCode = exception.GetAppErrorCode ();
		//int httpErrorCode = exception.GetHttpErrorCode ();

		if (exception.GetAppErrorCode () == 2002 || exception.GetHttpErrorCode () == 404) {
			jsonTxt = exception.GetMessage ();
			Debug.Log (jsonTxt);

		}
		//App42Log.SetDebug(true);

    }

}