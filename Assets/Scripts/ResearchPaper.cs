using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchPaper : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //PLayerController.score += 25;
            gameObject.GetComponent<PLayerController>().score +=25;
            Destroy(gameObject);
        }

    }
}
