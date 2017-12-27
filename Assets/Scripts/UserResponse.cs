using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.ComponentModel;


public class UserResponse : App42CallBack
{   
	Text errorMessage;
    void init()
    {
        errorMessage = GameObject.Find("warning").GetComponent<Text> ();
    } 

    public void OnSuccess(object response)
    {
		init ();
		Scene scene = SceneManager.GetActiveScene ();
		//App42Response app42response = (App42Response)response;
	
		errorMessage.text = "Success";
		if (scene.name.Equals ("login_menu"))
			errorMessage.text = "Success"; // Direct to map scene	
		else if (scene.name.Equals ("reg_menu")) {			
			SceneManager.LoadScene ("login_menu");

		}
        
		
    }

    public void OnException(Exception e)
    {
		init ();
		string jsonTxt = "";
        App42Exception exception = (App42Exception)e;
        Debug.Log(exception.GetAppErrorCode());
        int appErrorCode = exception.GetAppErrorCode();
        //int httpErrorCode = exception.GetHttpErrorCode ();

        if (appErrorCode == 2000)
        {
            jsonTxt = "Username not found!";
        }

		else if (appErrorCode == 2002)
		{
			jsonTxt = "Username/Password did not match. Authentication Failed!";
		}

		else if (appErrorCode == 2005)
		{
			jsonTxt = "Email Address already exists!";
		}

		else if (appErrorCode == 2001)
		{
			jsonTxt = "Username already exists!";
		}



					

		errorMessage.text = jsonTxt;

		Debug.Log(exception.GetMessage());
        //App42Log.SetDebug(true); //print output in editor console


    }

}