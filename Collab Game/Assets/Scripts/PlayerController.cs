using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //components
    Rigidbody2D rb;
    Vector2 moveDirection;

    //player
    float walkSpeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

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

        if (inputHorizontal == 0 && inputVertical == 0)
        {
            if (currentDirection == "Right") ChangeAnimationState(Idle_Right);
            if (currentDirection == "Left") ChangeAnimationState(Idle_Left);
            if (currentDirection == "Up") ChangeAnimationState(Idle_Back);
            if (currentDirection == "Down") ChangeAnimationState(Idle_Front);
        }
        else if (inputHorizontal > 0)
        { ChangeAnimationState(Move_Right); currentDirection = "Right"; }

        else if (inputHorizontal < 0)
        { ChangeAnimationState(Move_Left); currentDirection = "Left"; }

        else if (inputVertical < 0)
        { ChangeAnimationState(Move_Forward); currentDirection = "Down"; }

        else if (inputVertical > 0)
        { ChangeAnimationState(Move_Back); currentDirection = "Up"; }
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
