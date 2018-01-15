using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TrashManager {
    private static TrashManager Instance;
    private string CurrentTrash;
    private TrashManager()
    {
        CurrentTrash = "";
    }

    public static TrashManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new TrashManager();
            return Instance;
        }
    }

    public string GetTrash() { return CurrentTrash; }
    public void SetTrash(string trash) { this.CurrentTrash = trash; }
}
