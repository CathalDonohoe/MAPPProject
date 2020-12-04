using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{

    private Transform player;

    private float yOffset = 7f;
    private float zOffest = 4f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffest);
    }
}
