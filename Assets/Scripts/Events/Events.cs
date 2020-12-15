using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void ReplayGame(){
        //plays players death sound
        FindObjectOfType<AudioManager>().Stop("PlayerDeath");
        ScoreScript.scoreValue = 0;
        //sets player speed back to default
        PLayerController.forwardSpeed = 5f;
        //reloads scene
        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){
        //quits game
        Application.Quit();
    }
}
