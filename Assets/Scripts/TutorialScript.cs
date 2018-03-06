using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {

    GameObject tutorialPanel;
    int tutorialNo;
    static int currNo = 1;
    string scene;

    // Use this for initialization
    void Start () {        

        tutorialPanel = GameObject.Find("TutorialPanel");
        scene = SceneManager.GetActiveScene().name;
        if (scene.Equals("map")) tutorialNo = 1;
        else if (scene.Equals("trash_menu")) tutorialNo = 2;
        else if (scene.Equals("trash_search")) tutorialNo = 3;
        else if (scene.Equals("trash_seg")) tutorialNo = 4;        

        if (PlayerManager.GetInstance().GetPlayer().PlayerGameLvlNo != 1 || currNo != tutorialNo)
        {
            tutorialPanel.SetActive(false);
        }
    }
	
	public void NextTutorial()
    {
        currNo++;
        PlayerManager.GetInstance().SetTutorial(currNo);
    }

}
