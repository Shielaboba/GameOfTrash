﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashRandomManager {

    private static TrashRandomManager Instance;
    private List<TrashData> CurrentTrash;
    private TrashRandomManager()
    {
        //CurrentTrash = "";
    }

    public static TrashRandomManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new TrashRandomManager();
            return Instance;
        }
    }

    public List<TrashData> GetTrash() { return CurrentTrash; }
    public void SetTrash(List<TrashData> trash) { this.CurrentTrash = trash; }
}
