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
    public GameObject levelDetails, noLifeDetails;
    PlayerData player;
    public Text ScoreText;
    public Button PlayBtn, CloseBtn, OkayBtn, Close1Btn;
    // Use this for initialization
    void Start ()
    {
        ScoreScript.scorePoints = 0;
        levelDetails.SetActive(false);
        noLifeDetails.SetActive(false);
        int level = LevelManager.GetInstance().GetLevel();
        LvlBtn = int.Parse(btn.GetComponentInChildren<Text>().text);

        if (LvlBtn > level)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("PlayerCurrentLives") == 0)
        {
            noLifeDetails.SetActive(true);
            OkayBtn.onClick.AddListener(delegate ()
            {
                noLifeDetails.SetActive(false);
            });

            Close1Btn.onClick.AddListener(delegate ()
            {
                noLifeDetails.SetActive(false);
            });
        }
        else
        {
            levelDetails.SetActive(true);

            LevelManager.GetInstance().SetSelectLevel(LvlBtn);

            if (LevelManager.GetInstance().GetSelectLevel() == 1)
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[0] + "";
            else if (LevelManager.GetInstance().GetSelectLevel() == 2)
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[1] + "";
            else if (LevelManager.GetInstance().GetSelectLevel() == 3)
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[2] + "";
            else if (LevelManager.GetInstance().GetSelectLevel() == 4)
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[3] + "";
            else if (LevelManager.GetInstance().GetSelectLevel() == 5)
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[4] + "";
            else
                ScoreText.text = PlayerManager.GetInstance().GetPlayer().PlayerScoreLevel[5] + "";

            PlayBtn.onClick.AddListener(delegate ()
            {
                TrashRandom();
            });

            CloseBtn.onClick.AddListener(delegate ()
            {
                levelDetails.SetActive(false);
            });
        }

    }

    void TrashRandom()
    {
        Constant c;
        string diffValue, type;
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
