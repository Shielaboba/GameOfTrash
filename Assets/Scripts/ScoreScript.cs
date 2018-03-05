using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (scorePoints < 0)
            scorePoints = 0;

        scoreText.text = "Score: " + (player.PlayerScoreMade + scorePoints);
    }
        
    public static void AddPoints (int pointsToAdd)
    {
        scorePoints += pointsToAdd;
        PlayerPrefs.SetInt("PlayerCurrentScore", scorePoints);
    } 
}


