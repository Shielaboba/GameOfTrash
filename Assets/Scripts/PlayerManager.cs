﻿public class PlayerManager
{
    private static PlayerManager Instance;
    private PlayerData CurrentPlayer;
    private int Tutorial;

    private PlayerManager()
    {
        
    }

    public static PlayerManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new PlayerManager();
            return Instance;
        }
    }

    public PlayerData GetPlayer() { return CurrentPlayer; }
    public void SetPlayer(PlayerData CurrentPlayer) { this.CurrentPlayer = CurrentPlayer; }
    public void SetTutorial(int Tutorial) { this.Tutorial = Tutorial; }
    public int GetTutorial() { return Tutorial; }
}