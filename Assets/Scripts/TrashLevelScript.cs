using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using System.Linq;

public class TrashLevelScript : MonoBehaviour {

    public int level;
    public Button[] btn;
    Constant c;
    String key,value;

	// Use this for initialization
	void Start () {
        level = LevelManager.GetInstance().GetLevel();
        
        if (!gameObject.name.Equals("trashLevel"+level))
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
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        key = "TrashLvlDifficulty";
        value = "Easy";
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentByKeyValue("GOTDB", "TrashFile", key, value, new TrashLevelResponse()); 
    }


}

internal class TrashLevelResponse : App42CallBack
{
    public Button[] btn;
    TrashData[] trash;
    ArrayList randomNumbers;
    public Image img;
    int number;
    System.Random rnd;

    public void OnSuccess(object response)
    {
        int level = LevelManager.GetInstance().GetLevel();
        img = GameObject.Find("trashLevel"+level).GetComponent<Image>();
        btn = new Button[img.transform.childCount];
        rnd = new System.Random();
        randomNumbers = new ArrayList();

        Storage storage = (Storage)response;

        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
        trash = new TrashData[jsonDocList.Count];

        for (int i = 0; i < jsonDocList.Count && i < btn.Length; i++)
        {
            do
            {
                number = rnd.Next(0, jsonDocList.Count-1);
            }
            while (randomNumbers.Contains(number));

            randomNumbers.Add(number);
            Debug.Log("-" + number);

            trash[i] = JsonUtility.FromJson<TrashData>(jsonDocList[number].GetJsonDoc());            
            
            //Debug.Log("jsonDoc is " + jsonDocList[i].GetJsonDoc());
        }

        

        for (int i = 0; i < btn.Length; i++)
        {
            btn[i] = GameObject.Find("b" + (i + 1)).GetComponent<Button>();            
            btn[i].GetComponent<Button>().GetComponentInChildren<Text>().text = trash[i].TrashName;
        }


    }

    public void OnException(Exception ex)
    {
        Debug.Log(ex.Message);
    }

}