using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Walking, Idle, Engaging };

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int Health;
    [SerializeField] float Speed;

    [Header("Attack Stats")]
    [SerializeField] float BaseDamage;
    [SerializeField] float AttackCooldown;
    [SerializeField] GameObject Weapon;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myFeetCollider;
    float CurrentAttackCooldown;

    //Determines length of time till enemy can do a new action.
    //Such as walking or standing.
    int ActionCooldownAmount;
    int direction;
    State state;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mybodyCollider = GetComponent<CapsuleCollider2D>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        ActionCooldownAmount--;
        if (ActionCooldownAmount <= 0)
        {
            if (isIdle())
            {
                Walk();
            }
            else
            {
                Idle();
            }
            DetermineActionCooldownAmount();
        }

        if (mybodyCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            Idle();
            DetermineActionCooldownAmount();
            //myRigidBody.velocity
        }
        

        Vector2 enemyVelocity = new Vector2(direction * Speed, 0);
        myRigidBody.velocity = enemyVelocity;
    }

    void Idle()
    {
        state = State.Idle;
        myAnimator.SetBool("isMoving", false);
        direction = 0;
    }

    void Walk()
    {
        state = State.Walking;
        int action = Random.Range(0, 2);
        if (action == 0) { direction = -1; }
        else if (action == 1) { direction = 1; }

        //Have enemy walk around
        transform.localScale = new Vector2(direction, 1f);
        myAnimator.SetBool("isMoving", true);

        //Detect if it collides with other objects.
    }
    void DetermineActionCooldownAmount()
    {
        ActionCooldownAmount = Random.Range(30, 600);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Taking Damage");
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    void Die() //Death Animation + Destroy GameObject. May need to be a delayed call.
    {
        Destroy(this.gameObject);
    }

    bool isWalking()
    {
        if (state == State.Walking) return true;
        return false;
    }
    bool isIdle()
    {
        if (state == State.Idle) return true;
        return false;
    }
}
