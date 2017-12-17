using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenerateTrashMapLevel : MonoBehaviour {
    
    int Level;
    public Button btn;

	// Use this for initialization
	void Start () {
        Level = LevelManager.GetInstance().GetLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {       
        Level = LevelManager.GetInstance().GetLevel();
        int LvlBtn = int.Parse(btn.GetComponentInChildren<Text>().text);
        if (LvlBtn <= Level)
        {
            Debug.Log(btn.GetComponentInChildren<Text>().text);
            SceneManager.LoadScene("map_level");
            LevelManager.GetInstance().SetLevel(LvlBtn);
        }
        else
        {
            Debug.Log("X = " + btn.GetComponentInChildren<Text>().text);
        }
    }

    /*
    public void TrapLevel()
    {
        Level = LevelManager.GetInstance().GetLevel();
        for(int i = 0; i < LvlBtn.Length; i++)
        {
            if(!(LvlBtn[i].GetComponent<Text> ().ToString()).Equals(""+Level))
            {
                LvlBtn[i].SetActive(false);
            }
        }
    }
    */
}
