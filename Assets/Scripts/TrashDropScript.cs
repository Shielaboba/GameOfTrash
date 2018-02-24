using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System.Timers;
using UnityEngine.SceneManagement;

public class TrashDropScript : MonoBehaviour
{
    public GameObject[] obj;
    GameObject optionsPanel;
    GameObject replayPanel;
    Text timer;
    List<TrashData> trash;
    public float timeLeft;
    int selLevel, currLevel, nextLevel;

    private void Start()
    {
        nextLevel = LevelManager.GetInstance().GetLevel() + 1;
        selLevel = LevelManager.GetInstance().GetSelectLevel();
        currLevel = LevelManager.GetInstance().GetLevel();
        optionsPanel = GameObject.Find("optionsPanel");
        replayPanel = GameObject.Find("replayPanel");
        timer = GameObject.Find("timer").GetComponent<Text>();
        timeLeft = 30.0f;
        SelectLevelGame();
        
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        if (timeLeft >= 0)
        {
            replayPanel.SetActive(false);
            if (minutes == 0 && seconds <= 59)
                timer.color = Color.red;

            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            replayPanel.SetActive(true);
            Button btn = GameObject.Find("RepBtn").GetComponent<Button>();

            btn.onClick.AddListener(delegate ()
            {
                Destroy(this);
                SceneManager.LoadScene("map");
            });
        }

        if (transform.childCount-4 == 0)
        {
            optionsPanel.SetActive(true);
            Button btn = GameObject.Find("GoButton").GetComponent<Button>();
            
            btn.onClick.AddListener(delegate ()
            {
                if(selLevel == currLevel)
                    LevelManager.GetInstance().SetLevel(nextLevel);
                SceneManager.LoadScene("map");
            });
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    public void SelectLevelGame()
    {        
        if (!gameObject.name.Equals("TrashSegLevel" + selLevel))
        {                
            gameObject.SetActive(false);
        }
        else StartCoroutine("DeployTrash");
    }

    IEnumerator DeployTrash()
    {
        trash = TrashRandomManager.GetInstance().GetTrash();
        for (int i = 0; i < obj.Length; i++)
        {
            WWW www = new WWW(trash[i].TrashUrl);
            yield return www;

            if (www.isDone) obj[i].SetActive(true);
            obj[i].GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            obj[i].name = trash[i].TrashName;
            Instantiate(obj[i]);
        }
    }
}
