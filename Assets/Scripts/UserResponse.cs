using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;

public class UserResponse : App42CallBack
{
    private string result = "";
    void start()
    {

    }

    public void OnSuccess(object user)
    {
        try
        {
            User userObj = (User)user;
            result = userObj.ToString();
            Debug.Log("UserName : " + userObj.GetUserName());
            Debug.Log("EmailId : " + userObj.GetEmail());
        }
        catch (App42Exception e)
        {
        }
    }

    public void OnException(Exception e)
    {
        result = e.ToString();
        Debug.Log("Exception : " + e);
    }

    public string getResult()
    {
        return result;
    }

}