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

    public int score;
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
        Debug.Log(score);
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
        if(ScoreScript.scoreValue >=20){
            forwardSpeed = 4.5f;

            if (ScoreScript.scoreValue >= 30){
                forwardSpeed = 6f;

                if (ScoreScript.scoreValue >= 40){
                    forwardSpeed = 8f;
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
        onCoffee = true;
        wasSpeed = forwardSpeed;
        forwardSpeed = 10f;
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
