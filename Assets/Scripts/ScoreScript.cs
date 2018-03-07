using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public class ScoreScript : MonoBehaviour
{
    public static int scorePoints = 0;
    Text scoreText;
    PlayerData player;

    // Use this for initialization
    void Start()
    {
        player = PlayerManager.GetInstance().GetPlayer();
        scoreText = GetComponent<Text>();
        scorePoints = PlayerPrefs.GetInt("PlayerCurrentScore");

        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);

        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetLastScoreByUser(cons.gameName, player.PlayerName, new ScoreResponse());
    }

    // Update is called once per frame
    void Update()
    {
        if (scorePoints < 0)
            scorePoints = 0;
        
        scoreText.text = "Score: " + (PlayerPrefs.GetInt("PlayerTotalScore") + scorePoints);
    }
        
    public static void AddPoints (int pointsToAdd)
    {
        scorePoints += pointsToAdd;
        PlayerPrefs.SetInt("PlayerCurrentScore", scorePoints);
    } 
}


