using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public float wasSpeed;

    void OnTriggerEnter(Collider other)
    {
       /* if (other.tag == "Player")
        {
            wasSpeed = PLayerController.forwardSpeed;
            PLayerController.forwardSpeed = 25;
            new WaitForSeconds (5);
            PLayerController.forwardSpeed = wasSpeed;
            Destroy(other.gameObject);
        }*/
    }
}
