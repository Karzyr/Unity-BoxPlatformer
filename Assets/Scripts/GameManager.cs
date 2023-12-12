using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Player Score
    public int score = 0;

    // static instance of the GM can be accessed from anywhere
    public static GameManager instance;

    //Per Level Score
    public int levelScore = 0;
    
    //Coin Total Amount
    public int coinTotalAmount;

    //HighScore
    public int highScore = 0;

    //CurrentLevel
    private int currentLevel = 1;

    //Amount of Levels
    public int levelAmount = 2;

    //set HUDManager
    private HUDManager hudManager;

    // Start is called before the first frame update
    void Awake()
    {
        //check if GameManager exists
        if (instance == null)
        {
            //Assign to current object
            instance = this;

            //don't destroy this object when changing scenes
            DontDestroyOnLoad(gameObject);

            //Find Object
            hudManager = FindObjectOfType<HUDManager>();


            coinTotalAmount = GameObject.FindGameObjectsWithTag("Coin").Length;
            levelScore = 0;
        }
        //Check if it exists
        else if (instance != this)
        {
            //Find Object
            instance.hudManager = FindObjectOfType<HUDManager>();
            
            instance.coinTotalAmount = GameObject.FindGameObjectsWithTag("Coin").Length;
            instance.levelScore = 0;

            //Destroy the current game object - we only need 1
            Destroy(gameObject);
        }
    }



    //increase the player score

    public void IncreaseScore(int amount)
    {

        //increase score
        score += amount;
        levelScore += amount;

        //Compare HighScore
        if (score > highScore) highScore = score;

        //show the new score
        print("New score: " + score + "HighScore: " + highScore);

        //update HUD Manager
        //if(hudManager != null) 
        hudManager.ResetHud();
    }

    public void ResetGame()
    {
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            //update HUD Manager
            hudManager.ResetHud();
        }


        //reset Score
        score = 0;

        //set currentLevel
        currentLevel = 1;

        //load level
        SceneManager.LoadScene("Level1");
    }

    public void GameOver()
    {
        //load level
        SceneManager.LoadScene("GameOver");
    }

    public void NextLevel()
    {
        //check if there are more levels
        if (currentLevel < levelAmount)
        {
            currentLevel++;
        }
        else currentLevel = 1;

        SceneManager.LoadScene("Level" + currentLevel);
    }


}
