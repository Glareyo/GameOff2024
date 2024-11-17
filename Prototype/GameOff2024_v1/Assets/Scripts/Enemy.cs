using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Walking, Idle, Engaging, Dead };

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int Health;
    [SerializeField] float Speed;
    [SerializeField] float DetectionRange;

    [Header("Attack Stats")]
    [SerializeField] float BaseDamage;
    [SerializeField] int AttackCooldown;
    [SerializeField] GameObject Weapon;

    [Header("Behaviour Modifiers")]
    [SerializeField] int minActionCooldown;
    [SerializeField] int maxActionCooldown;
    public int ActionCooldownAmount;

    int CurrentAttackCooldown;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myFeetCollider;
    EdgeCollider2D mySightCollider;

    GameObject Player;


    //Determines length of time till enemy can do a new action.
    //Such as walking or standing.
    int direction;
    State state;
    bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mybodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        mySightCollider = GetComponent<EdgeCollider2D>();

        SetMySightColliderPoints();

        state = State.Idle;
        isAlerted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Dead) { return; }

        ActionCooldownAmount--;
        if (ActionCooldownAmount <= 0)
        {
            if (!isAlerted) { Patrol(); }
            DetermineActionCooldownAmount();
        }

        if (!isAlerted)
        {
            if (mybodyCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                Idle();
                DetermineActionCooldownAmount();
            }
        }
        
        if (isWalking())
        {
            Vector2 enemyVelocity = new Vector2(direction * Speed, 0);
            myRigidBody.velocity = enemyVelocity;
        }
        

        if (isPlayerWithinRange())
        {
            if (!isAlerted) { Alerted(); }
            Shoot();
        }
    }

    /// <summary>
    /// Engage the player in combat
    /// </summary>
    void Engage(Collider2D collision)
    {
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        //Exit if not alerted
        //if (!isAlerted) return;

        *//*if (collision.gameObject.tag == "Player")
        {

        }*//*
    }*/

    /// <summary>
    /// Detects Player
    /// </summary>
    /// <param name="collision"></param>
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player SPotted");
            //If the enemy is not already alerted of the player, then become alerted.
            if (!isAlerted) Alerted();
            if(isAlerted)
            {
                Shoot();
            }
        }
    }*/

    bool isPlayerWithinRange()
    {
        if (mySightCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Player Sighted");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Changes state to alerted and gets player object
    /// </summary>
    void Alerted()
    {
        isAlerted = true;
        myAnimator.SetBool("isEngaging", true);
        myAnimator.SetBool("isMoving", false);
    }


    /// <summary>
    /// Set a Patrol state, walking or idleing
    /// </summary>
    void Patrol()
    {
        if (isIdle())
        {
            Walk();
        }
        else
        {
            Idle();
        }
    }


    /// <summary>
    /// Idle
    /// </summary>
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
        SetMySightColliderPoints();

        myAnimator.SetBool("isMoving", true);
    }

    /// <summary>
    /// Shoot or attack with their weapon.
    /// </summary>
    void Shoot()
    {
        state = State.Engaging;
        myAnimator.SetTrigger("Shoot");
    }

    /// <summary>
    /// Determines amount of time till it can take the next action
    /// </summary>
    void DetermineActionCooldownAmount()
    {
        ActionCooldownAmount = Random.Range(minActionCooldown, maxActionCooldown);
    }

    void SetMySightColliderPoints()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(mySightCollider.points[0]);
        points.Add(new Vector2(DetectionRange, mySightCollider.points[0].y));
        


        mySightCollider.SetPoints(points);
    }

    public void TakeDamage(int damage)
    {
        //If already dead, exit function
        if (isDead()) { return; }
        
        //Reduce health from damage.
        Health -= damage;
        if (Health <= 0)
        {
            //If health <= 0, then die.
            Die();
        }
    }
    void Die() //Death Animation + Destroy GameObject. May need to be a delayed call.
    {
        myAnimator.SetTrigger("Die");
        mybodyCollider.enabled = false;
        myFeetCollider.enabled = false;

        myRigidBody.bodyType = RigidbodyType2D.Static;
        
        state = State.Dead;
        //Destroy(this.gameObject);
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
    bool isDead()
    {
        return state == State.Dead;
    }
}
