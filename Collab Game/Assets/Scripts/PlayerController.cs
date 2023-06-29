using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //components
    Rigidbody2D rb;
    Vector2 moveDirection;

    //player
    float runspeed = 6f;
    float walkSpeed = 3f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;
    float sprint;

    // Anmiations and states
    Animator animator;
    string currentState;
    const string Idle_Right = "Idle Animation Right";
    const string Idle_Left = "Idle Animation Left";
    const string Idle_Back = "Idle Animation Back";
    const string Idle_Front = "Idle Animation Front";
    const string Move_Right = "Move Right Animation";
    const string Move_Left = "Move Left Animation";
    const string Move_Back = "Move Back Animation";
    const string Move_Forward = "Move Forward Animation";

    string currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {
        ProcessInputs();
    }

    private void FixedUpdate()

    {
        Move();
    }

    public void ProcessInputs()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(inputHorizontal, inputVertical).normalized;

    }

    public void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * walkSpeed, moveDirection.y * walkSpeed);


        if (inputHorizontal > 0)
        {
            currentDirection = "Right";
            if (inputHorizontal > 3) ChangeAnimationState(Move_Right);
            else ChangeAnimationState(Idle_Right);
        }

        else if (inputHorizontal < 0)
        {
            currentDirection = "Left";
            if(inputHorizontal < -3) ChangeAnimationState(Move_Left);
            else ChangeAnimationState(Idle_Left);
        }
        
        else if (inputVertical < 0)
        {
            currentDirection = "Down";
            if (inputVertical < -3) ChangeAnimationState(Move_Forward);
            else ChangeAnimationState(Idle_Front);
        }

        else if (inputVertical > 0)
        {
            currentDirection = "Up";
            if (inputVertical > 3) ChangeAnimationState(Move_Back);
            else ChangeAnimationState(Idle_Back);
        }
    }

    // Animation State Changer
    void ChangeAnimationState(string newState)

        // Stop Animation Interrupting Itself
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
