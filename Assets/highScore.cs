using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class highScore : MonoBehaviour
{
    public GameObject hSUI;
    public GameObject MainMenuUI;
    public Text highestScore;

    // Update is called once per frame
    public void showHighScore()
    {
        MainMenuUI.SetActive(false);
        hSUI.SetActive(true);
        highestScore.text = "High Score is: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void back()
    {
        MainMenuUI.SetActive(true);
        hSUI.SetActive(false);
    }
}
