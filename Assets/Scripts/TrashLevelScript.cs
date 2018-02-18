using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;

public class TrashLevelScript : MonoBehaviour {

    int level, count;
    Constant c;
    String key,value;
    Button[] btn;
    public Sprite mybutton;
    List<TrashData> trash;

    // Use this for initialization
    void Start()
    {
        level = LevelManager.GetInstance().GetLevel();
        count = TrashRandomManager.GetInstance().GetTrash().Count;
        trash = TrashRandomManager.GetInstance().GetTrash();

        btn = new Button[level];
        btn = gameObject.GetComponentsInChildren<Button>();

        if (!gameObject.name.Equals("trashLevel" + level))
        {
            print(level);
            gameObject.SetActive(false);

        }

        for (int i = 0; i < count; i++)
        {
            btn[i].GetComponentInChildren<Text>().text = trash[i].TrashName;

            if (trash[i].CheckTrash)
            {
               
                btn[i].enabled = false;
                btn[i].image.overrideSprite = mybutton;
                btn[i].GetComponentInChildren<Text>().color = Color.gray;
            }

            int copy = i;
            btn[i].onClick.AddListener(delegate () {
                OnClick(trash[copy]);
            });
        }
    }

    public void OnClick(TrashData trash)
    {
        TrashManager.GetInstance().SetTrash(trash);
        SceneManager.LoadScene("trash_search");
    }
}