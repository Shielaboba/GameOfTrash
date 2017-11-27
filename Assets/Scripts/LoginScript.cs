using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class LoginScript : MonoBehaviour {

	string userField, passField;
	InputField username, pass;
	Text errorMessage;

	// Use this for initialization
	void Start () {		
		username = GameObject.Find("username").GetComponent<InputField>();
		pass = GameObject.Find("password").GetComponent<InputField>();
		errorMessage = GameObject.Find("warning").GetComponent<Text> ();
	}

	public bool fieldCheck() {
		userField = username.text;
		passField = pass.text;

		if (!userField.Equals("") && !passField.Equals(""))
		{
			return true;
		}

		errorMessage.text = "Please fill all blanks.";

		return false;
	}

	public void loginBtn() {
		if (fieldCheck())
		{			
			Constant cons = new Constant();
			App42API.Initialize(cons.apiKey, cons.secretKey);
			UserService userService = App42API.BuildUserService();
			try
			{
				userService.Authenticate(userField, passField, new UserResponse());
			}
			catch (App42Exception e)
			{

			};
			App42Log.SetDebug(true);
		}
	}
}
