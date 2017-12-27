using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class RegisterScript : MonoBehaviour
{

    string userField, passField, confPassField, emailField;
    InputField username, pass, confPass, email;
    Text errorMessage;
	EventSystem system;
    

    void Start()
    {
        username = GameObject.Find("username").GetComponent<InputField>();
        pass = GameObject.Find("pass").GetComponent<InputField>();
        confPass = GameObject.Find("confpass").GetComponent<InputField>();
		email = GameObject.Find ("email").GetComponent<InputField> ();
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
		system = EventSystem.current;
		email.ActivateInputField();
    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable> ().FindSelectableOnDown ();

			if (next != null) {
				InputField inputfield = next.GetComponent<InputField> ();

				if (inputfield != null)
					inputfield.OnPointerClick (new PointerEventData (system));

				system.SetSelectedGameObject (next.gameObject, new BaseEventData (system));
			}
		}
	}

    public bool FieldCheck()
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
                errorMessage.text = "Password didn't match";
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

    public void RegisterBtn()
    {
        if (FieldCheck())
        {

            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            UserService userService = App42API.BuildUserService();

            try
            {
                userService.CreateUser(userField, passField, emailField, new UserResponse(userField));
            }
            catch (App42Exception e)
            {
				errorMessage.text = "" + e.Message;
            };
            //App42Log.SetDebug(true);
        }
    }
}