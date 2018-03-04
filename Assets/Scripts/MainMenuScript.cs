using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject window;
    public Text messageField;

    public void Play()
    {
        SceneManager.LoadScene("login_menu"); 
    }

    public void ShowAbout()
    {
        window.SetActive(true);
    }
    public void HideAbout()
    {
        window.SetActive(false);
    }

}
