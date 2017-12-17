using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapScript: MonoBehaviour {

    public int level;
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public Sprite level5;
    public GameObject background;

    void Start()
    {
        UpdateLevel();
    }

    void Update()
    {               
        
    }

    public void UpdateLevel() {
        this.level = LevelManager.GetInstance().GetLevel();

        switch (level)
        {
            case 1: background.GetComponent<Image>().sprite = level1; break;
            case 2: background.GetComponent<Image>().sprite = level2; break;
            case 3: background.GetComponent<Image>().sprite = level3; break;
            case 4: background.GetComponent<Image>().sprite = level4; break;
            case 5: background.GetComponent<Image>().sprite = level5; break;
        }
    }
}
