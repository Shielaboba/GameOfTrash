using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameDisplay : MonoBehaviour {

    Text username;
    
    void Start () {
        String userName = PlayerPrefs.GetString("username");
        username = GetComponent<Text>();
        username.text = userName;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
