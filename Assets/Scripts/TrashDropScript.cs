using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;
using UnityEngine.SceneManagement;

public class TrashDropScript : MonoBehaviour
{
    public GameObject[] obj;
    List<TrashData> trash;

    private void Start()
    {
        SelectLevelGame();
        
    }

    private void Update()
    {        
        if (transform.childCount-4 == 0)
        {            
            print("PROCEED TO NEXT LEVEL!");
            LevelManager.GetInstance().SetLevel(LevelManager.GetInstance().GetLevel()+1);
            SceneManager.LoadScene("map");
        }
    }

    public void SelectLevelGame()
    {
        int level = LevelManager.GetInstance().GetSelectLevel();

        if (!gameObject.name.Equals("TrashSegLevel" + level))
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
