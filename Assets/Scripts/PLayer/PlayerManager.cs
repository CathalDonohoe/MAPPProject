using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;


    //plays game
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
    }

    
    void Update()
    {
        //shows game over screen
        if (gameOver)
        {
            //Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        //if the player taps the screen starts game
        if(SwipeManager.tap){
            isGameStarted = true;
            Destroy(startingText);
        }
    }

}
