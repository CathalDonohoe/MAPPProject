using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void ReplayGame(){
        //FindObjectOfType<AudioManager>().Playsound("Replay");
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){
        //FindObjectOfType<AudioManager>().Playsound("Quit");
        Application.Quit();
    }
}
