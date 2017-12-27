using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Popout : MonoBehaviour {

    public GameObject window;
    public Text messageField;
	

    public void Show(string message)
    {
        message = "";
        messageField.text = message;
        window.SetActive(true);
    }

    public void Hide()
    {
      
        window.SetActive(false);
    }
}
