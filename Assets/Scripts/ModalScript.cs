using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalScript : MonoBehaviour {


    public GameObject window;

    public void Show()
    {
        window.SetActive(true);
    }

    public void Hide()
    {
        window.SetActive(false);
    }
}
