using System.Collections;
using System.Collections.Generic;
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
        userName.text = uname;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
