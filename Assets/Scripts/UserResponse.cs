using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;
using SimpleJSON;


public class UserResponse : App42CallBack
{   
	Text errorMessage;

    String user;
    String collectionName, key, value;
    public UserResponse() { }

    public UserResponse ( String user)
    {
       
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
        this.user = user;
    }
   
    public void OnSuccess(object response)
    {		
		Scene scene = SceneManager.GetActiveScene ();		
	
		errorMessage.text = "Success";
        if (scene.name.Equals("login_menu"))
        {
            errorMessage.text = "Success";
            
            new ProgressLoadScript(user).LoadProgress();
        }
        else if (scene.name.Equals("reg_menu"))
        {
            JSONClass json = new JSONClass
            {
                { "PlayerGameLvlNo", 1 },
                { "PlayerName", user }
            };
            StorageService storageService = App42API.BuildStorageService();
            storageService.InsertJSONDocument("GOTDB", "PerformanceFile", json, new UserResponse());
            SceneManager.LoadScene("login_menu");
        }
        
    }

    public void OnException(Exception e)
    {
		String jsonTxt = "";
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
    }

}
