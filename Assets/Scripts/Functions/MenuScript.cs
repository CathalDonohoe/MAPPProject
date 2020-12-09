using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame(){
        //FindObjectOfType<AudioManager>().Playsound("Play");
        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){
        //FindObjectOfType<AudioManager>().Playsound("Quit");
        Application.Quit();
    }
}
