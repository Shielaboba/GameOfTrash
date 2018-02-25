using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutScript : MonoBehaviour
{
    string sessionID = UserResponse.sessionID;
    public void Logout()
    {
        SceneManager.LoadScene("login_menu");
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        UserService userService = App42API.BuildUserService();
        userService.Logout(sessionID, new LogoutResponse());
    }
}
