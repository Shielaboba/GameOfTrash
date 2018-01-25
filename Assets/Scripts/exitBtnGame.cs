using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitBtnGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

            
    }
}
