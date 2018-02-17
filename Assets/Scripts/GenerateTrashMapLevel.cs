using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System;

public class GenerateTrashMapLevel : MonoBehaviour {
    
    int level;
    public Button btn;
    int LvlBtn;

    // Use this for initialization
    void Start ()
    {
        level = LevelManager.GetInstance().GetLevel();
        LvlBtn = int.Parse(btn.GetComponentInChildren<Text>().text);
 
        if (LvlBtn > level)
        {
            btn.gameObject.SetActive(false);
        }
    }
	
    public void OnClick()
    {       
        Debug.Log(btn.GetComponentInChildren<Text>().text);
        TrashRandom();
    }

    void TrashRandom()
    {
        Constant c;
        string key, value;

        if (level == 1 || level == 2) value = "easy";
        else value = "hard";
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        key = "TrashLvlDifficulty";
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentByKeyValue("GOTDB", "TrashFile", key, value, new TrashLevelResponse());
    }

}

internal class TrashLevelResponse : App42CallBack
{
    List<TrashData> trash;
    ArrayList randomNumbers;
    int number;
    System.Random rnd;

    public void OnSuccess(object response)
    {
        int trashHolder = 4;
        
        int level = LevelManager.GetInstance().GetLevel();

        if(level > 1) trashHolder += (level*2) - 2;

        rnd = new System.Random();
        randomNumbers = new ArrayList();

        Storage storage = (Storage)response;

        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
        trash = new List<TrashData>();
        for (int i = 0; i < trashHolder; i++)
        {
            do
            {
                number = rnd.Next(0, jsonDocList.Count - 1);
            } while (randomNumbers.Contains(number));

            randomNumbers.Add(number);
            trash.Add(JsonUtility.FromJson<TrashData>(jsonDocList[number].GetJsonDoc()));
        }
        var trashRandom = TrashRandomManager.GetInstance();
        trashRandom.SetTrash(trash);
        SceneManager.LoadScene("trash_menu");
    }

    public void OnException(Exception ex)
    {
        Debug.Log(ex.Message);
    }
}
