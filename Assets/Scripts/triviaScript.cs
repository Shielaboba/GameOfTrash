﻿using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.SceneManagement;

public class TriviaScript : MonoBehaviour {

    // Use this for initialization
    string collectionName = "TrashFile";
    string keyName = "TrashName";
    Text btnText;
    TrashData trash;
    LifeManager lifeManager;
    private void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        trash = TrashManager.GetInstance().GetTrash();
        btnText = GameObject.Find("BtnDiy").GetComponentInChildren<Text>();
    }

    public void Show()
    {
        if (gameObject.GetComponentInChildren<Text>().text.ToUpper().Equals(trash.TrashSegType.ToUpper()))
        {
            ScoreScript.scorePoints += 10;
            SceneManager.LoadScene("trivia_menu");
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            Query query = QueryBuilder.Build(keyName, trash.TrashName, Operator.EQUALS);
            StorageService storageService = App42API.BuildStorageService();
            storageService.FindDocumentsByQuery(cons.dbName, collectionName, query, new TriviaResponse());

            if (SceneManager.GetActiveScene().name.Equals("trivia_menu"))
                GameObject.Find("triviaModalImg").SetActive(true);                
        }
        else
        {
            lifeManager.TakeLife(); 
            GameObject.Find("Title").GetComponent<Text>().text = "Incorrect Type";
        }           
    }

    public void Hide()
    {
        GameObject.Find("triviaModalImg").SetActive(false);
    }

    public void OnClick()
    {
        trash.CheckTrash = true;
        if(trash.TrashRecyclable.ToUpper().Equals("YES"))
            SceneManager.LoadScene("DIY");
        else
            SceneManager.LoadScene("trash_menu");        
    }
}
