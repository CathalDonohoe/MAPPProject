﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 2; //distance between  2 lanes

    public float jumpForce;
    public float gravity = -20;

    public int score;
    public float wasSpeed= 0 ;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(score);

        direction.z = forwardSpeed;
        
        if (controller.isGrounded){
            direction.y = -1;
            if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
            }
        }else{
            direction.y += gravity*Time.deltaTime;

        }

        

        //gather input on which lane we should be in

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            desiredLane++;
            if(desiredLane==3){
                desiredLane = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            desiredLane--;
            if(desiredLane==-1){
                desiredLane = 0;
            }
        }


        //calculate where we should be in the fututre
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane==0){
            targetPosition +=Vector3.left * laneDistance;
        }else if (desiredLane == 2){
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.deltaTime);
        controller.center = controller.center;
    }

    private void FixedUpdate(){
        controller.Move(direction*Time.fixedDeltaTime);
    }

    private void Jump(){
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }

        if (hit.transform.tag == "Coffee")
        {
            wasSpeed = forwardSpeed;
            forwardSpeed = 25;
            new WaitForSeconds (5);
            forwardSpeed = wasSpeed;
            Destroy(hit.transform.gameObject);
        }

        if (hit.transform.tag == "Homework")
        {
            score +=5;
            Destroy(hit.transform.gameObject);
        }

        if (hit.transform.tag == "Research Paper")
        {
            score += 25;
            Destroy(hit.transform.gameObject);
        }
    }
}
