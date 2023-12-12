using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        scoreText.text = "Score: " + gameManager.score;
        highScoreText.text = "HighScore: " + gameManager.highScore;
    }

    //Restart Game
    public void RestartGame()
    {
        gameManager.ResetGame();
        SceneManager.LoadScene("Level1");
    }


}
