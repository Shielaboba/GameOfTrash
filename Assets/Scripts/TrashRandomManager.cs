﻿using System.Collections.Generic;

public class TrashRandomManager {

    private static TrashRandomManager Instance;
    private List<TrashData> CurrentTrash;
    private List<DIYTrashData> DIYTrash;

    private TrashRandomManager()
    {
        
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

    public List<DIYTrashData> GetDIYTrash() { return DIYTrash; }
    public void SetDIYTrash(List<DIYTrashData> DIYTrash) { this.DIYTrash = DIYTrash; }

}
