using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class Certification : MonoBehaviour
{
    String uname;
    void Awake()
    {
        uname = PlayerPrefs.GetString("username");
    }
    public void email()
    {
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);
        UserService userService = App42API.BuildUserService();
        userService.GetUser(uname, new emailResponse());
        Debug.Log("Email Sent");
    }

    internal class emailResponse : App42CallBack
    {
        public void OnSuccess(object response)
        {
            User user = (User)response;
            SendEmail(user.email, user.userName);
            Debug.Log(user.email + " " + user.userName);
        }
        public void OnException(Exception ex)
        {
            Debug.Log("Unsuccessful");
        }

        public void SendEmail(String email, String uname)
        {
            // String email, uname;
            // email = PlayerPrefs.GetString("email");
            // uname = PlayerPrefs.GetString("username");
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("devzygote101@gmail.com");
            mail.To.Add("" + email);
            Debug.Log(email);
            mail.Subject = "Game Of Trash";
            mail.Body = "Congratulations " + uname + ",\nYou have completed all the levels of the game. You are now a responsible citizen! \nThere are more updates to come. Stay tuned!   \n\n" +
                "-------------------\n" + "Best Regards,\n" + "Team Zygote";

            Attachment attachment;
            attachment = new Attachment("assets/img/eggshell.jpg");
            mail.Attachments.Add(attachment);

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
    }
}
