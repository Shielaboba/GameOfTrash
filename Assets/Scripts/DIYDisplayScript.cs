using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DIYDisplayScript : MonoBehaviour
{

    // Use this for initialization
    List<DIYTrashData> diy = TrashRandomManager.GetInstance().GetDIYTrash();
    Text craftname1, craftname2, craftnameDisplay, tools, steps;
    GameObject procedure;
    string btnName;

    void Start()
    {
        try
        {
            procedure = GameObject.Find("procedure");
            craftname1 = GameObject.Find("name_diy1").GetComponent<Text>();
            craftname2 = GameObject.Find("name_diy2").GetComponent<Text>();

            craftnameDisplay = GameObject.Find("name_diy").GetComponent<Text>();
            tools = GameObject.Find("prepareText").GetComponent<Text>();
            steps = GameObject.Find("Procedure").GetComponent<Text>();
            procedure.SetActive(false);

            DisplayDIYMain();
        }
        catch (Exception e)
        {

        }
    }

    public void DisplayDIYMain()
    {
        craftname1.text = diy[0].DIYCraftName;
        craftname2.text = diy[1].DIYCraftName;
        StartCoroutine(FindImage(diy[0].DIYCraftName, "BtnSample1"));
        StartCoroutine(FindImage(diy[1].DIYCraftName, "BtnSample2"));        
    }

    public void InitDisplay()
    {       
        procedure.SetActive(true);
    }

    public void DisplayPro(string btn)
    {
        string toolsholder = "";
        string stepsholder = "";
        InitDisplay();
        
        for (int i = 0; i < diy.Count; i++)
        {
            if (btn.Equals(diy[i].DIYCraftName))
            {
                craftnameDisplay.text = diy[i].DIYCraftName;
                for (int x = 0; x < diy[i].DIYTools.Length; x++)
                {
                    toolsholder = toolsholder + diy[i].DIYTools[x] + "\n";
                }

                for (int y = 0; y < diy[i].DIYProcedure.Length; y++)
                {

                    stepsholder = stepsholder + diy[i].DIYProcedure[y] + "\n";
                }

                tools.text = toolsholder;
                steps.text = stepsholder;
                StartCoroutine(FindImage(diy[i].DIYCraftName, "img_diy"));
                break;
            }

        }                
    }

    public void OnClick()
    {
        SceneManager.LoadScene("trash_menu");
    }

    public void DisplayProcedure1()
    {        
        string toolsholder = "";
        string stepsholder = "";
        InitDisplay();
        craftnameDisplay.text = diy[0].DIYCraftName;

        for(int i=0; i<diy.Count; i++)
        {
            if (craftnameDisplay.text.Equals(diy[i].DIYCraftName))
            {
                for(int x=0; x<diy[i].DIYTools.Length; x++)
                {

                    toolsholder = toolsholder + diy[i].DIYTools[x] + "\n";
                }

                for (int y = 0; y < diy[i].DIYProcedure.Length; y++)
                {

                    stepsholder = stepsholder + diy[i].DIYProcedure[y] + "\n";
                }

                tools.text = toolsholder;
                steps.text = stepsholder;
                break;
            }
        }
        
        StartCoroutine(FindImage(diy[0].DIYCraftName, "img_diy"));
        
    }

    public void DisplayProcedure2()
    {        
        string toolsholder = "";
        string stepsholder = "";
        InitDisplay();
        craftnameDisplay.text = diy[1].DIYCraftName;

        for (int i = 0; i < diy.Count; i++)
        {
            if (craftnameDisplay.text.Equals(diy[i].DIYCraftName))
            {
                for (int x = 0; x < diy[i].DIYTools.Length; x++)
                {

                    toolsholder = toolsholder + diy[i].DIYTools[x] + "\n";
                }

                for (int y = 0; y < diy[i].DIYProcedure.Length; y++)
                {

                    stepsholder = stepsholder + diy[i].DIYProcedure[y] + "\n";
                }

                tools.text = toolsholder;
                steps.text = stepsholder;
                break;
            }
        }

        StartCoroutine(FindImage(diy[1].DIYCraftName, "img_diy"));

    }

    IEnumerator FindImage(string craft, string imageName)
    {        
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Ocp-Apim-Subscription-Key", "1b4d9849f0e14b3a815c56c5958832be");

            var url = "https://api.cognitive.microsoft.com/bing/v7.0/search?q=" + craft + "&answerCount=1&responseFilter=images&mkt=en-us";

            using (WWW www = new WWW(url, null, headers))
            {
                yield return www;

                if (string.IsNullOrEmpty(www.error))
                {
                    JsonData picUrl = JsonMapper.ToObject(www.text);
                    using (WWW w = new WWW(picUrl["images"]["value"][0]["thumbnailUrl"].GetString()))
                    {
                        yield return w;
                        GameObject.Find(imageName).GetComponent<Image>().sprite = Sprite.Create(w.texture, new Rect(0, 0, w.texture.width, w.texture.height), new Vector2(0, 0));
                    }
                }
                else
                {
                    Debug.Log("Error: " + www.error);
                }
            }
    }

}
