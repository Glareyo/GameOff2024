using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int Health;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpSpeed;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myFeetCollidor;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollidor = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        if (myFeetCollidor.IsTouchingLayers(LayerMask.GetMask("Platform")) && myRigidbody.velocity.y == 0)
        {
            myAnimator.SetBool("IsFalling", false);
        }
        else if (!myFeetCollidor.IsTouchingLayers(LayerMask.GetMask("Platform")) && myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("IsJumping", false);
            myAnimator.SetBool("IsFalling", true);
            myAnimator.SetBool("IsRunning", false);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        myRigidbody.velocity += new Vector2(0f,JumpSpeed);
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("IsJumping", playerHasVerticalSpeed);
    }
    void Run()
    {
        //Movement
        Vector2 playerVelocity = new Vector2(moveInput.x * PlayerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }
}
