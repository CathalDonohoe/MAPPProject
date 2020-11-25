using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homework : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            PLayerController.instance.Homework();
            Destroy(gameObject);
        }

    }
}
