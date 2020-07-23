using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{

    public float walkSpeed = 8;
    public float runSpeed = 18;
    public float gravity = -12;

    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    //To make the player move with the camera
    public Transform cameraT;
    public GameObject cam;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {

            cam.SetActive(true);
            // SetPlayerCaption(DBManager.username);

        
        animator = GetComponent<Animator>();
        //      cameraT = Camera.main.gameObject.transform;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    //Creating a vector2 for keyboard input
    void Update()
    {

        //--------------------INPUT----------------
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //lets take the input vector and turn it into direction
        Vector2 InputDir = input.normalized;
        bool running = (Input.GetKey(KeyCode.RightShift));


        //lets now make the character move to that direction
        //shift key is to run
        Move(InputDir, running);

        //------------ANIMATOR-------------------
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("Blend", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    void Move(Vector2 InputDir, bool running)
    {



        //So, so only calculate the direction if the inputDir is not 0 0.
        if (InputDir != Vector2.zero)
        {


            float targetRotation = Mathf.Atan2(InputDir.x, InputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        //this depends on if we are running or not. So if running then use speed 18 otherwise 8
        //if no input then speed will be 0
        float speed = ((running) ? runSpeed : walkSpeed) * InputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        //this is to move the character
        Vector3 Velocity = transform.forward * currentSpeed + Vector3.up * velocityY; ;

        controller.Move(Velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;//if character not moving then make it stand still

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

    }




}