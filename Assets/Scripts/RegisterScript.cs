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

    void Start()
    {
        Debug.Log("hey");
        username = GameObject.Find("username").GetComponent<InputField>();
        pass = GameObject.Find("pass").GetComponent<InputField>();
        confPass = GameObject.Find("confpass").GetComponent<InputField>();
        email = GameObject.Find("email").GetComponent<InputField>();
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
            Debug.Log("q");
        }

        if (passField.Equals(confPassField)) flag = true;

        return flag;
    }

    public void registerBtn()
    {
        Debug.Log("yey");
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
                
            };
            App42Log.SetDebug(true);
        }
    }

}