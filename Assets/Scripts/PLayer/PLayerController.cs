using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public static float forwardSpeed;

    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 1.7f; //distance between  2 lanes

    public float jumpForce;
    public float gravity = -20;

    public float wasSpeed;

    public bool onCoffee = false;
    public bool isSliding = false;

    public static bool isDead = false;
    private Animator anim;

    public GameObject Level2UI;
    public GameObject Level3UI;
    public GameObject FLevelUI;
    
    public bool inlevel2;
    public bool inlevel3;
    public bool inFinalLevel;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        forwardSpeed = 5f;
        inlevel2 = true;
        inlevel3 = true;
        inFinalLevel = true;
        isSliding = false;

        StartCoroutine(addScore());
        ScoreScript.scoreValue = 0;
        FindObjectOfType<AudioManager>().Play("Theme");

    }
    
    void Update()
    {        
        if(!PlayerManager.isGameStarted){
            return;
        }

        //to update score
        CheckScore();
        //moves the player along the z axis
        direction.z = forwardSpeed;
        
        //only allows the player to jump if theyre touching the ground
        if (controller.isGrounded){
            if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
            }
            if(SwipeManager.swipeUp){
                Jump();
            }
        }else{
            //enforces gravity
            direction.y += gravity*Time.deltaTime;

        }

        //gather input on which lane we should be in
        //and which they will be put into based on their input and current lane
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

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            //can only slide when not sliding
            if(isSliding==false){
                StartCoroutine(Slide());
            }
            else{
                return;
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

    //used to manage the players score
    private void CheckScore(){
        //this is the score limit for level 1
        if(ScoreScript.scoreValue >=700){
            StartCoroutine(Level2());
            inlevel2 = false;
            forwardSpeed = 6.5f;

            if(onCoffee == true){
                StartCoroutine(Speed()); 
            }

            //this is the score limit for level 2
            if (ScoreScript.scoreValue >= 1800){
               
                StartCoroutine(Level3());
                inlevel3 = false;
                forwardSpeed = 8f;

                if(onCoffee == true){
                StartCoroutine(Speed()); 
                }

                //this is the score limit for level 3
                if (ScoreScript.scoreValue >= 2400){
                    
                    StartCoroutine(FLevel());
                    inFinalLevel = false;
                    forwardSpeed = 10f;

                    if(onCoffee == true){
                        StartCoroutine(Speed()); 
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

    //checks the level theyre in to avoid entering state multiple times
    IEnumerator Level2()
    {
        if(inlevel2 == true)
        {
            //changes theme
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("Level2");
            //splash screen
            Level2UI.SetActive(true);
            yield return new WaitForSeconds(1);
            Level2UI.SetActive(false);
        }
        

    }

    //checks the level theyre in to avoid entering state multiple times
    IEnumerator Level3()
    {
        if(inlevel3 == true)
        {
            //changes theme
            FindObjectOfType<AudioManager>().Stop("Level2");
            FindObjectOfType<AudioManager>().Play("Level3");
            //splash screen
            Level3UI.SetActive(true);
            yield return new WaitForSeconds(1);
            Level3UI.SetActive(false);
        }
        
    }

    //checks the level theyre in to avoid entering state multiple times
    IEnumerator FLevel()
    {
        if(inFinalLevel == true)
        {
            //changes theme
            FindObjectOfType<AudioManager>().Stop("Level3");
            FindObjectOfType<AudioManager>().Play("FLevel");
            //splash screen
            FLevelUI.SetActive(true);
            yield return new WaitForSeconds(1);
            FLevelUI.SetActive(false);
        }

    }

    //Used for the coffee pickup
    IEnumerator Speed()
    {
        //saves current speed to a variable and then adds 5 to the speed
        wasSpeed = forwardSpeed;
        forwardSpeed += 5f;
        yield return new WaitForSeconds(5);
        //sets speed back
        forwardSpeed = wasSpeed;
        onCoffee = false;

    }

    //used for the slide mechanic
    IEnumerator Slide()
    {
        //sets the animimation to true
        isSliding=true;
        anim.SetBool("Slide", true);
        //changes the controllers dimensions and position to avoid collision
        controller.center = new Vector3(0,0.5f,0);
        controller.height = 0.01f;
        controller.radius = 0.1f;
        
        yield return new WaitForSeconds(0.7f);
        //sets animation back to run
        anim.SetBool("Slide", false);
        //sets controller position and dimesions back
        controller.center = new Vector3(0,0.85f,0);
        controller.height = 2;
        controller.radius = 0.3f;
        isSliding=false;

    }

    //Used for the death animation
    IEnumerator Dead()
    {
        anim.SetTrigger("NowDead");
        //direction.y -=6.5f;
        yield return new WaitForSeconds(2);
    }

    //Used to add 10 to score every 10 seconds
    IEnumerator addScore()
    {
        ScoreScript.scoreValue += 10;
        yield return new WaitForSeconds(10);
        StartCoroutine(addScore());
    }

    //used ofr all collisions with player
    private void OnControllerColliderHit(ControllerColliderHit hit){
        //obstacles kill the player
        if(hit.transform.tag == "Obstacle")
        {
            //plays death sound and starts Dead Ienumerator
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            forwardSpeed = 0f;
            StartCoroutine(Dead()); 
            PlayerManager.gameOver = true;
        }

        //for table obstacle
        if(hit.transform.tag == "Table")
        {
            //player survives if in sliding animation
            if(isSliding == true){
                Debug.Log("Slide");
                return;
            }
            //else kills like other obstacles
            else{
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                forwardSpeed = 0f;
                StartCoroutine(Dead()); 
                PlayerManager.gameOver = true;
            }
            
        }


        //coffee increases speed through the coffee IEnumerator
        if (hit.transform.tag == "Coffee")
        {
            FindObjectOfType<AudioManager>().Play("Coffee");
            if(onCoffee == false){
                onCoffee = true;
                StartCoroutine(Speed()); 
                Destroy(hit.transform.gameObject);
            }

            else{
                Destroy(hit.transform.gameObject);
            }
        }


        //homework adds to player score
        if (hit.transform.tag == "Homework")
        {
            FindObjectOfType<AudioManager>().Play("Homework");
            ScoreScript.scoreValue +=5;
            Destroy(hit.transform.gameObject);
        }

        //research paper also adds to score
        if (hit.transform.tag == "Research paper")
        {
            FindObjectOfType<AudioManager>().Play("Research Paper");
            ScoreScript.scoreValue += 25;
            Destroy(hit.transform.gameObject);
        }
    }
}
