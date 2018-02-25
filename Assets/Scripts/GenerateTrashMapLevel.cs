using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System;

public class GenerateTrashMapLevel : MonoBehaviour {
    
    public Button btn;
    int LvlBtn;

    // Use this for initialization
    void Start ()
    {
        int level = LevelManager.GetInstance().GetLevel();
        LvlBtn = int.Parse(btn.GetComponentInChildren<Text>().text);
 
        if (LvlBtn > level)
        {
            btn.gameObject.SetActive(false);
        }
    }
	
    public void OnClick()
    {
        LevelManager.GetInstance().SetSelectLevel(LvlBtn);
        TrashRandom();
    }

    void TrashRandom()
    {
        Constant c;
        string key, diffValue, type;
        Query query, query1, query2;
        int level = LevelManager.GetInstance().GetSelectLevel();

        if (level == 1 || level == 2)
        {
            diffValue = "easy";
            query = QueryBuilder.Build("TrashLvlDifficulty", diffValue, Operator.EQUALS);            
        }
        else if (level == 3 || level == 4)
        {
            diffValue = "easy";
            type = "residual";
            query1 = QueryBuilder.Build("TrashLvlDifficulty", diffValue, Operator.EQUALS);
            query2 = QueryBuilder.Build("TrashSegType", type, Operator.EQUALS);
            query = QueryBuilder.CompoundOperator(query1, Operator.OR, query2);
        }
        else
        {
            query1 = QueryBuilder.Build("TrashLvlDifficulty", "easy", Operator.EQUALS);
            query2 = QueryBuilder.Build("TrashLvlDifficulty", "hard", Operator.EQUALS);
            query = QueryBuilder.CompoundOperator(query1, Operator.OR, query2);
        }
            
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        key = "TrashLvlDifficulty";
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentsByQuery("GOTDB", "TrashFile", query, new TrashLevelResponse());
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
        
        int level = LevelManager.GetInstance().GetSelectLevel();

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
