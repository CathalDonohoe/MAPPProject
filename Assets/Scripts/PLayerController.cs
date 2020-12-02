using System.Collections;
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

    public float wasSpeed;

    public bool onCoffee = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if(!PlayerManager.isGameStarted){
            return;
        }

        CheckScore();
        Debug.Log(forwardSpeed);

        direction.z = forwardSpeed;
        
        if (controller.isGrounded){
            direction.y = -1;
            if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
            }
            if(SwipeManager.swipeUp){
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
        if(SwipeManager.swipeRight){
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
        if(SwipeManager.swipeLeft){
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

    private void CheckScore(){
        if(ScoreScript.scoreValue >=700){
            forwardSpeed = 6.5f;

            if(onCoffee == true){
                StartCoroutine(Wait()); 
            }

            if (ScoreScript.scoreValue >= 1800){
                forwardSpeed = 8f;

                if(onCoffee == true){
                StartCoroutine(Wait()); 
                }

                if (ScoreScript.scoreValue >= 2400){
                    forwardSpeed = 10f;

                    if(onCoffee == true){
                        StartCoroutine(Wait()); 
                    }
                }
            }
        }

    }

    private void FixedUpdate(){

        if(!PlayerManager.isGameStarted){
            return;
        }
        controller.Move(direction*Time.fixedDeltaTime);
    }

    private void Jump(){
        direction.y = jumpForce;
    }

    IEnumerator Wait()
    {
        wasSpeed = forwardSpeed;
        forwardSpeed += 5f;
        yield return new WaitForSeconds(5);
        forwardSpeed = wasSpeed;
        onCoffee = false;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }

        if (hit.transform.tag == "Coffee")
        {
            if(onCoffee == false){
                onCoffee = true;
                StartCoroutine(Wait()); 
                Destroy(hit.transform.gameObject);
            }

            else{
                Destroy(hit.transform.gameObject);
            }
        }

        if (hit.transform.tag == "Homework")
        {
            ScoreScript.scoreValue +=5;
            Destroy(hit.transform.gameObject);
        }

        if (hit.transform.tag == "Research paper")
        {
            ScoreScript.scoreValue += 25;
            Destroy(hit.transform.gameObject);
        }
    }
}
