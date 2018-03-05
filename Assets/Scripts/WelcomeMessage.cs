using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WelcomeMessage : MonoBehaviour {

    Text message;
    string uname;

	// Use this for initialization
	void Start ()
    {
        uname = PlayerPrefs.GetString("username");
        message = GetComponent<Text>();
        message.text = "Welcome "+uname+" to Game of Trash!";

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
