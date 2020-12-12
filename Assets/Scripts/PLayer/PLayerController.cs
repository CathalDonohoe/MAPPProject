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

    }
    
    void Update()
    {        
        if(!PlayerManager.isGameStarted){
            return;
        }

        CheckScore();
        //Debug.Log(forwardSpeed);

        direction.z = forwardSpeed;
        
        if (controller.isGrounded){
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

        if(Input.GetKeyDown(KeyCode.DownArrow)){
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

    private void CheckScore(){
        if(ScoreScript.scoreValue >=700){
            
            StartCoroutine(Level2());
            inlevel2 = false;
            forwardSpeed = 6.5f;

            if(onCoffee == true){
                StartCoroutine(Speed()); 
            }

            if (ScoreScript.scoreValue >= 1800){
               
                StartCoroutine(Level3());
                inlevel3 = false;
                forwardSpeed = 8f;

                if(onCoffee == true){
                StartCoroutine(Speed()); 
                }

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

    IEnumerator Level2()
    {
        if(inlevel2 == true)
        {
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("Level2");
            Level2UI.SetActive(true);
            yield return new WaitForSeconds(1);
            Level2UI.SetActive(false);
        }
        

    }

    IEnumerator Level3()
    {
        if(inlevel3 == true)
        {
            FindObjectOfType<AudioManager>().Stop("Level2");
            FindObjectOfType<AudioManager>().Play("Level3");
            Level3UI.SetActive(true);
            yield return new WaitForSeconds(1);
            Level3UI.SetActive(false);
        }
        
    }

    IEnumerator FLevel()
    {
        if(inFinalLevel == true)
        {
            FindObjectOfType<AudioManager>().Stop("Level3");
            FindObjectOfType<AudioManager>().Play("FLevel");
            FLevelUI.SetActive(true);
            yield return new WaitForSeconds(1);
            FLevelUI.SetActive(false);
        }

    }

    IEnumerator Speed()
    {
        wasSpeed = forwardSpeed;
        forwardSpeed += 5f;
        yield return new WaitForSeconds(5);
        forwardSpeed = wasSpeed;
        onCoffee = false;

    }
    IEnumerator Slide()
    {
        isSliding=true;
        anim.SetBool("Slide", true);
        controller.center = new Vector3(0,0.5f,0);
        controller.height = 0.01f;
        controller.radius = 0.1f;
        
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("Slide", false);
        controller.center = new Vector3(0,0.85f,0);
        controller.height = 2;
        controller.radius = 0.3f;
        isSliding=false;

    }

    //failure at animation
    IEnumerator Dead()
    {
        anim.SetTrigger("NowDead");
        direction.y -=6.5f;
        yield return new WaitForSeconds(2);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.transform.tag == "Obstacle")
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            forwardSpeed = 0f;
            StartCoroutine(Dead()); 
            PlayerManager.gameOver = true;
        }
        if(hit.transform.tag == "Table")
        {
            if(isSliding == true){
                Debug.Log("Slide");
                return;
            }
            else{
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                forwardSpeed = 0f;
                StartCoroutine(Dead()); 
                PlayerManager.gameOver = true;
            }
            
        }

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

        if (hit.transform.tag == "Homework")
        {
            FindObjectOfType<AudioManager>().Play("Homework");
            ScoreScript.scoreValue +=5;
            Destroy(hit.transform.gameObject);
        }

        if (hit.transform.tag == "Research paper")
        {
            FindObjectOfType<AudioManager>().Play("Research Paper");
            ScoreScript.scoreValue += 25;
            Destroy(hit.transform.gameObject);
        }
    }
}
