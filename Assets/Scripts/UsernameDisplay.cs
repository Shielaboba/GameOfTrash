using UnityEngine;
using UnityEngine.UI;
using System;

public class UsernameDisplay : MonoBehaviour {

    Text userName;
    String uname;

	// Use this for initialization
	void Start () {
        uname= PlayerPrefs.GetString("username");
        userName = GetComponent<Text>();
        userName.text = "Welcome " + uname;
    }
}
