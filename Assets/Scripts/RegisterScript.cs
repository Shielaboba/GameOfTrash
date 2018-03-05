using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.game;
using SimpleJSON;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp.email;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class RegisterScript : MonoBehaviour
{
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
        if (username.text.Equals(string.Empty) || pass.text.Equals(string.Empty) || confPass.text.Equals(string.Empty) || email.text.Equals(string.Empty))
        {
            errorMessage.text = "Please fill all blanks.";
            return false;
        }
        
        if (!pass.text.Equals(confPass.text))
        {
            errorMessage.text = "Password didn't match";
            return false;
        }       

        if(pass.text.Length < 6 || confPass.text.Length < 6)
        {
            errorMessage.text = "Password too short. Must be atleast 6 characters";
            return false;
        }
        
        return true;
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
                userService.CreateUser(username.text, pass.text, email.text, new RegisterResponse());
                PlayerPrefs.SetString("email", email.text);
                PlayerPrefs.SetString("uname", username.text);
                PlayerPrefs.Save();
            }
            catch (App42Exception e)
            {
                errorMessage.text = char.ToUpper(e.Message[0]) + e.Message.Substring(1).ToLower();
            };            
        }
    }
}

internal class RegisterResponse : App42CallBack
{
    public void OnSuccess(object response)
    {
        Text errorMessage = GameObject.Find("warning").GetComponent<Text>();
        User user = (User)response;  
        
        string levelScore = "[{\"Level1\": 0}," +
                                "{ \"Level2\": 0}," +
                                "{ \"Level3\": 0}," +
                                "{ \"Level4\": 0}," +
                                "{ \"Level5\": 0}," +
                                "{ \"Level6\": 0}]";

        JSONClass json = new JSONClass
            {
                { "PlayerGameLvlNo", 1 },
                { "PlayerName", user.userName },
                { "PlayerScoreMade", 0 },
                { "PlayerPowerLife", 1},
                { "PlayerPowerScore", 1},
                { "PlayerLife", 5 },
                { "PlayerScoreLevel", levelScore}
            };

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.SaveUserScore("GOT", user.userName, 0, new Response()); // FOR SAVING FIRST SCORE FOR JUST REGISTERED PLAYERS.
        errorMessage.text = "Successfully registered!";
        SendEmail(user.userName, user.email);
        StorageService storageService = App42API.BuildStorageService();
        storageService.InsertJSONDocument("GOTDB", "PerformanceFile", json, new Response());
        SceneManager.LoadScene("login_menu");
    }

    public void SendEmail(String uname, String email)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("devzygote101@gmail.com");
        mail.To.Add("" + email);
        Debug.Log(email);
        mail.Subject = "Game Of Trash";
        mail.Body = "Hi " + uname + ",\nYou have successfully registered to the Game Of Trash where learning how to segregate and recycle trash are made in a fun and interactive way! You can now proceed to the game and start playing. Welcome and enjoy! \n\n" +
            "-------------------\n" + "Best Regards,\n" + "Team Zygote";

        SmtpClient smtpServer = new SmtpClient();
        smtpServer.Host = "smtp.gmail.com";
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("devzygote101@gmail.com", "zygote101") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Send(mail);
        Debug.Log("success");
    }

    public void OnException(Exception ex)
    {
        String jsonTxt = "";
        Text errorMessage = GameObject.Find("warning").GetComponent<Text>();
        App42Exception exception = (App42Exception)ex;
        
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