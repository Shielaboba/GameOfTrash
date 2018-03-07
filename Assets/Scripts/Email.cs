using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine.UI;

public class Email : MonoBehaviour {
    String uname, email;
    Text errorMessage;
    
    public void SendEmail()
    {
        uname = PlayerPrefs.GetString("uname");
        email = PlayerPrefs.GetString("email");

        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("devzygote101@gmail.com");
        mail.To.Add(""+email);
        Debug.Log(email);
        mail.Subject = "Game Of Trash";
        mail.Body = "Hi "+uname+",\n"+"\n\tYou have successfully registered to the Game Of Trash where learning how to segregate and recycle trash are made in a fun and interactive way! You can now proceed to the game and start playing. Welcome and enjoy! \n\n"+
            "-------------------\n"+"Best Regards,\n"+"Team Zygote";
 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("devzygote101@gmail.com", "zygote101") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = 
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
            {
                 return true;
            };
        smtpServer.Send(mail);
        Debug.Log("success");
         
     }
    
}

