using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class RegisterScript : MonoBehaviour
{

    string userField, passField, confPassField, emailField;
    InputField username, pass, confPass, email;
    Text errorMessage;

    

    void Start()
    {
        username = GameObject.Find("username").GetComponent<InputField>();
        pass = GameObject.Find("pass").GetComponent<InputField>();
        confPass = GameObject.Find("confpass").GetComponent<InputField>();
        email = GameObject.Find("email").GetComponent<InputField>();
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
        
    }

    public bool fieldCheck()
    {
        bool flag = false;

        userField = username.text;
        passField = pass.text;
        confPassField = confPass.text;
        emailField = email.text;

        if (!userField.Equals("") && !passField.Equals("") && !confPassField.Equals("") && !emailField.Equals(""))
        {
            flag = true;
            errorMessage.text = "";

            if (passField.Equals(confPassField))
            {
                flag = true;
                errorMessage.text = "";
            }
            else
            {
                errorMessage.text = "Password don't match";
                errorMessage.color = Color.red;
                flag = false;
            }
        }
        else
        {
            errorMessage.text = "Please fill all blanks.";
            errorMessage.color = Color.red;
            flag = false;
        }

        return flag;
    }

    public void registerBtn()
    {
        if (fieldCheck())
        {

            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            UserService userService = App42API.BuildUserService();

            try
            {
                userService.CreateUser(userField, passField, emailField, new UserResponse());
            }
            catch (App42Exception e)
            {
                errorMessage.text = "" + e;
            };
            App42Log.SetDebug(true);
        }
    }
}