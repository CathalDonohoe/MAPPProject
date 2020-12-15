using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame(){
        //loads level 1
        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){
        //quits app
        Application.Quit();
    }
}
