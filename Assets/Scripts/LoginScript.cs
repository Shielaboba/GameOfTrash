using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class LoginScript : MonoBehaviour
{

    string userField, passField;
    System.String jsonTxt; 
    InputField username, pass;
    Text errorMessage;

    // Use this for initialization
    void Start()
    {
        username = GameObject.Find("username").GetComponent<InputField>();
        pass = GameObject.Find("password").GetComponent<InputField>();
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
    }

    public bool fieldCheck()
    {
        userField = username.text;
        passField = pass.text;

        if (!userField.Equals("") && !passField.Equals(""))
        {
            errorMessage.text = "";
            return true;
        }

        errorMessage.text = "Please fill all blanks.";
        errorMessage.color = Color.red;

        return false;
    }

    public void loginBtn()
    {

        if (fieldCheck())
        {
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            UserService userService = App42API.BuildUserService();
           
            userService.Authenticate(userField, passField, new UserResponse());
        }
    }
           
}

