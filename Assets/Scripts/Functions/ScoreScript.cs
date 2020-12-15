using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//script used for updating score and printing it to the UI
public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;

    void Start()
    {

        score = GetComponent<Text> ();

    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
        SaveHighScore(scoreValue);

    }

    private bool SaveHighScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log(highScore);
        bool gotNewHighScore = newScore > highScore;

        if (gotNewHighScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save();
        }

        return gotNewHighScore;
    }

}
