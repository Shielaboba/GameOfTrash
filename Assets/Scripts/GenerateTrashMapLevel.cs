using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenerateTrashMapLevel : MonoBehaviour {
    
    int Level;
    public Button btn;
    int LvlBtn;

    // Use this for initialization
    void Start ()
    {
        Level = LevelManager.GetInstance().GetLevel();
        LvlBtn = int.Parse(btn.GetComponentInChildren<Text>().text);
        if (LvlBtn > Level)
        {
            btn.gameObject.SetActive(false);
            //btn.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {       
        Debug.Log(btn.GetComponentInChildren<Text>().text);
        SceneManager.LoadScene("trash_menu");
        LevelManager.GetInstance().SetLevel(LvlBtn);
    }

}
