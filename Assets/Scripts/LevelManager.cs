using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager  {

    private static LevelManager Instance;
    private int CurrentLevel;
    private int SelectLevel;
    private LevelManager()
    {
        CurrentLevel = 1;
        SelectLevel = 1;
    }

    public static LevelManager GetInstance()
    {
        if(Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new LevelManager();
            return Instance;
        }
    }

    public int GetLevel() { return CurrentLevel; }
    public void SetLevel(int level) { this.CurrentLevel = level; }

    public int GetSelectLevel() { return SelectLevel; }
    public void SetSelectLevel(int level) { this.SelectLevel = level; }
}
