﻿using System.Collections;
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
    Boolean finishAllTrash = true;
    GameObject optionsPanel;

    // Use this for initialization
    void Start()
    {

        level = LevelManager.GetInstance().GetSelectLevel();
        count = TrashRandomManager.GetInstance().GetTrash().Count;
        trash = TrashRandomManager.GetInstance().GetTrash();
        optionsPanel = GameObject.Find("optionsPanel");
        //btn = new Button[level];
        btn = gameObject.GetComponentsInChildren<Button>();

        if (!gameObject.name.Equals("trashLevel" + level))
        {
            gameObject.SetActive(false);

        }

        for (int i = 0; i < count; i++)
        {
            btn[i].GetComponentInChildren<Text>().text = trash[i].TrashName;
            if (!trash[i].CheckTrash) finishAllTrash = false;
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

        if (finishAllTrash)
        {
            Button btn = GameObject.Find("GoButton").GetComponent<Button>();
            btn.onClick.AddListener(delegate ()
            {
                SceneManager.LoadScene("trash_seg");
            });           
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    public void OnClick(TrashData trash)
    {
        TrashManager.GetInstance().SetTrash(trash);
        SceneManager.LoadScene("trash_search");
    }

}