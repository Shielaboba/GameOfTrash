using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class LoginScript : MonoBehaviour
{
    string userField, passField;
    System.String jsonTxt; 
    InputField username, pass;
    Text errorMessage;
	EventSystem system;

    // Use this for initialization
    void Start()
    {
		username = GameObject.Find ("username").GetComponent<InputField> ();
        pass = GameObject.Find("password").GetComponent<InputField>();
        errorMessage = GameObject.Find("warning").GetComponent<Text>();
		system = EventSystem.current;
		username.ActivateInputField();
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

    public void LoginBtn()
    {

        if (FieldCheck())
        {
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            UserService userService = App42API.BuildUserService();
           
			try {
				
            	userService.Authenticate(userField, passField, new UserResponse(userField));
			}
			catch (App42Exception e)
			{
				errorMessage.text = "" + e.Message;
			};
        }
    }
           
}

