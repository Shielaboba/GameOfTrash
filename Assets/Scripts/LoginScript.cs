using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class LoginScript : MonoBehaviour
{
    String jsonTxt; 
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
        if (username.text.Equals(string.Empty) || pass.text.Equals(string.Empty))
        {
            errorMessage.text = "Please fill all blanks.";
            return false;
        }
        
        return true;
    }

    public void LoginBtn()
    {
        if (FieldCheck())
        {
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.Save();
            
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            UserService userService = App42API.BuildUserService();
           
			try {
                errorMessage.text = "Successfully logged in!";
                userService.Authenticate(username.text, pass.text, new LoginResponse());
			}
			catch (App42Exception e)
			{
				errorMessage.text = "" + e.Message;
			};
        }
    }  
}

internal class LoginResponse : App42CallBack
{
    public static string sessionID;
    Constant c;

    public void OnSuccess(object response)
    {
        c = new Constant();        
        User user = (User)response;
        sessionID = user.GetSessionId(); //Get session ID

        App42API.Initialize(c.apiKey, c.secretKey);
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentByKeyValue("GOTDB", "PerformanceFile", "PlayerName", user.userName, new PerformanceResponse());
    }
    public void OnException(Exception e)
    {
        String jsonTxt = "";
        Text errorMessage = GameObject.Find("warning").GetComponent<Text>();
        App42Exception exception = (App42Exception)e;

        int appErrorCode = exception.GetAppErrorCode();

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


