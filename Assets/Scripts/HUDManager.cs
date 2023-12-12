using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    //Score Text Label
    private Text scoreLabel;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameManager.instance;
       ResetHud();
    }

    //Show up to date stats of the player
    public void ResetHud()
    {
        scoreLabel = GameObject.Find("Score Text").GetComponent<Text>();
        scoreLabel.text = "Score: " + gameManager.score;
       
    }
}
