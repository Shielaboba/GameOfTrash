using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{

    GameObject Page1, Page2, EndText;
    // Button btnPrev,btnNext;
    Text textNext;
    Boolean flag1;
    public int count;
    // Use this for initialization
    void Start()
    {
        Page1 = GameObject.Find("page1");
        Page2 = GameObject.Find("page2");
        EndText = GameObject.Find("endText");
        count = 0;
        flag1 = false;
        textNext = GameObject.Find("nxtText").GetComponent<Text>();
        //btnPrev.enabled = false;
        Button btnPrev = GameObject.Find("prevBtn").GetComponent<Button>();
        Button btnNext = GameObject.Find("nextBtn").GetComponent<Button>();
        btnPrev.enabled = false;


        btnNext.onClick.AddListener(delegate ()
        {

            count++;
            if (count == 1)
            {
                Page1.SetActive(false);
                btnPrev.enabled = true;
            }

            if (count == 2)
            {
                Page2.SetActive(false);
                btnPrev.enabled = true;
                textNext.text = "GO TO MAP";
            }

            if (count == 3)
            {
                btnPrev.enabled = true;
                SceneManager.LoadScene("map");
            }




        });

        btnPrev.onClick.AddListener(delegate ()
        {
            count--;
            if (count == 1)
            {
                Page2.SetActive(true);
                textNext.text = "Next";
            }
            if (count == 0)
            {
                btnPrev.enabled = false;
                Page1.SetActive(true);
            }
        });



    }


}
