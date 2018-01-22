using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
//using Newtonsoft.Json.Linq;
public class UserResponse : App42CallBack
{   
	Text errorMessage;
	String jsonTxt;

    void start()
    {
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
    } 

    public void OnSuccess(object response)
    {
        
        App42Response app42response = (App42Response)response;
        String jsonResponse = app42response.ToString();
        errorMessage.text = "Success";
        Debug.Log("YOW" + jsonResponse);
    }

    public void OnException(Exception e)
    {
        App42Exception exception = (App42Exception)e;
        //Debug.Log(exception.GetAppErrorCode());
        int appErrorCode = exception.GetAppErrorCode();
        //int httpErrorCode = exception.GetHttpErrorCode ();

        if (appErrorCode == 2002 || appErrorCode == 404 ||
            appErrorCode == 2001 || appErrorCode == 2005)
        {
            errorMessage.text = "" + jsonTxt;
        }
        //Debug.Log(jsonTxt);
        //App42Log.SetDebug(true); //print output in editor console


    }

}