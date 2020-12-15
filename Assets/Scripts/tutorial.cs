using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject tutUI;
    public GameObject mainUI;

    
    void Start()
    {
        StartCoroutine(tut());
    }

    //causes the tutorial splash screen to appear to the player on start up
    IEnumerator tut(){
        mainUI.SetActive(false);
        tutUI.SetActive(true);
        yield return new WaitForSeconds(5);
        tutUI.SetActive(false);
        mainUI.SetActive(true);
    }

    
    //used for other scripts
    public void tutOn(){
        mainUI.SetActive(false);
        tutUI.SetActive(true);
    }

    public void tutOff(){
        tutUI.SetActive(false);
        mainUI.SetActive(true);
    }
}
