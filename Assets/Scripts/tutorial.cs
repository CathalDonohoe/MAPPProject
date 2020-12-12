using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject tutUI;
    public GameObject mainUI;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tut());
    }

    IEnumerator tut(){
        mainUI.SetActive(false);
        tutUI.SetActive(true);
        yield return new WaitForSeconds(5);
        tutUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void tutOn(){
        mainUI.SetActive(false);
        tutUI.SetActive(true);
    }

    public void tutOff(){
        tutUI.SetActive(false);
        mainUI.SetActive(true);
    }
}
