using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashDropHolder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool mouseDown = false;
    private Vector3 startMousePos;
    private Vector3 startPos;


    public void OnPointerDown(PointerEventData ped)
    {
        mouseDown = true;
        startPos = transform.position;
        startMousePos = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        mouseDown = false;
    }


    void Update()
    {
        if (mouseDown)
        {
            Vector3 currentPos = Input.mousePosition;
            Vector3 diff = currentPos - startMousePos;
            Vector3 pos = startPos + diff;
            transform.position = pos;
        }
    }

}