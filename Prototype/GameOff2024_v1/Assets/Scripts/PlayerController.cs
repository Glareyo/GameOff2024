using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int Health;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpSpeed;

    [Header("Player Body Parts")]
    [SerializeField] PlayerArms Arms;
    [SerializeField] PlayerBody Body;
    [SerializeField] PlayerHead Head;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    BoxCollider2D myFeetCollidor;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollidor = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        LookUp();
        if (myFeetCollidor.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            if (myRigidbody.velocity == Vector2.zero)
            {
                Body.Idle();
            }
        }
        else if (!myFeetCollidor.IsTouchingLayers(LayerMask.GetMask("Platform")) && myRigidbody.velocity.y < 0)
        {
            Body.Fall();
        }
    }

    void OnSwapWeapons()
    {
        Arms.SwapWeapons();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        myRigidbody.velocity += new Vector2(0f,JumpSpeed);
        Body.Jump();
    }

    void LookUp()
    {
        bool playerIsLookingUp = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        if (playerIsLookingUp)
        {
            Head.LookUp(false);
        }
        else
        {
            Head.LookDown();
        }
    }

    void Run()
    {
        //Movement
        Vector2 playerVelocity = new Vector2(moveInput.x * PlayerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            FlipSprite();
            Body.Run();
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }
}
