using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.SceneManagement;

public class TrashLevelScript : MonoBehaviour {

    int level;
    Constant c;
    String key,value;

    // Use this for initialization
    void Start()
    {
        level = LevelManager.GetInstance().GetLevel();

        if (!gameObject.name.Equals("trashLevel" + level))
        {
            gameObject.SetActive(false);
        }
        else
        {
            TrashRandom();            
        }
    }

    void TrashRandom()
    {
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
    Button[] btn;
    TrashData[] trash;
    ArrayList randomNumbers;    
    int number;
    System.Random rnd;
    Image img;

    public void OnSuccess(object response)
    {
        int level = LevelManager.GetInstance().GetLevel();        
        img = GameObject.Find("trashLevel"+level).GetComponent<Image>();
        btn = new Button[img.transform.childCount];
        btn = img.GetComponentsInChildren<Button>();
        rnd = new System.Random();
        randomNumbers = new ArrayList();        

        Storage storage = (Storage)response;

        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();        
        trash = new TrashData[btn.Length];
        
        for (int i = 0; i < btn.Length; i++)
        {            
            do {
				number = rnd.Next (0, jsonDocList.Count - 1);
			} while (randomNumbers.Contains (number));

			randomNumbers.Add (number);								

			trash [i] = JsonUtility.FromJson<TrashData> (jsonDocList [number].GetJsonDoc ());            
            
            btn[i].GetComponentInChildren<Text>().text = trash[i].TrashName;            

            int copy = i;
            btn[i].onClick.AddListener(delegate () { StartGame(trash[copy].TrashName); });            
        }        
    }

    void StartGame(string trash)
    {
        TrashManager.GetInstance().SetTrash(trash);        
        SceneManager.LoadScene("trash_search");        
    }

    public void OnException(Exception ex)
    {
        Debug.Log(ex.Message);
    }    
}
