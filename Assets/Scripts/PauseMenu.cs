using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        //function for player to pause on keyboard
        if (Input.GetKeyDown(KeyCode.Escape))    
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    //sets time to 1 and hides the pause menu
    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //this function pauses the time and shows the pause menu
    public void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Changes scene to main menu
    public void LoadMenu(){
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Stop("Level2");
        FindObjectOfType<AudioManager>().Stop("Level3");
        FindObjectOfType<AudioManager>().Stop("FLevel");
        SceneManager.LoadScene("Main Menu");
    }

    //quits appplication
    public void Quit(){
        Application.Quit();
    }
}
