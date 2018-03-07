using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {

    GameObject tutorialPanel;
    int tutorialNo, level;
    static int currNo = 1;
    string scene;
    static bool tutorial;

    // Use this for initialization
    void Start() {

        tutorialPanel = GameObject.Find("TutorialPanel");
        scene = SceneManager.GetActiveScene().name;
        level = PlayerManager.GetInstance().GetPlayer().PlayerGameLvlNo;

        if ((level != 1 && !tutorial) || currNo == 5)
        {
            currNo = 0;
            tutorial = false;
        }        

        if (scene.Equals("map")) tutorialNo = 1;
        else if (scene.Equals("trash_menu")) tutorialNo = 2;
        else if (scene.Equals("trash_search")) tutorialNo = 3;
        else if (scene.Equals("trash_seg")) tutorialNo = 4;        

        if (currNo != tutorialNo)
        {
            tutorialPanel.SetActive(false);
        }
    }
	
	public void NextTutorial()
    {
        currNo++;
    }

    public void BeginTutorial()
    {
        currNo = 1;
        tutorial = true;
    }

}
