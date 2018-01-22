using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject window;
    public Text messageField;

    public void scenes (string name)
    {
        SceneManager.LoadScene(name); 
    }

    //show window popout
    public void Show(string message)
    {
        messageField.text = message;
        window.SetActive(true);
    }
    //hide window
    public void Hide()
    {

        window.SetActive(false);
    }

}
