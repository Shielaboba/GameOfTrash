using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UsernameDisplay : MonoBehaviour {

    Text userName;

	// Use this for initialization
	void Start () {
        userName.text = PlayerPrefs.GetString("username");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
