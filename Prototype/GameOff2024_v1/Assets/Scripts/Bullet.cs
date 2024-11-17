using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;

    int damage;
    string shooterTag;
    Rigidbody2D myRigidBody;
    Vector2 Trajectory;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(Speed * Trajectory.x, Speed * Trajectory.y);
    }

    public void SetBullet(Vector2 trajectory, int Damage, string ShooterTag)
    {
        damage = Damage;
        Trajectory = trajectory;
        shooterTag = ShooterTag;

        if (trajectory.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D OtherCollider)
    {
        string colliderTag = OtherCollider.gameObject.tag;

        //If Colliding object is not the shooter itself, then figure out what it hit.
        if (colliderTag != shooterTag)
        {
            Destroy(gameObject);
            if (colliderTag == "Player")
            {
                PlayerController c = OtherCollider.gameObject.GetComponent<PlayerController>();
                //Take Damage
            }
            else if (colliderTag == "Enemy")
            {
                Enemy e = OtherCollider.gameObject.GetComponent<Enemy>();
                e.TakeDamage(damage);
            }
        }
        //Check layer of other collider.
        //Ensure that bullet isn't hitting the shooter
    }

    private void OnCollisionEnter2D(Collision2D OtherCollider)
    {
        string colliderTag = OtherCollider.gameObject.tag;
        
        if (colliderTag == shooterTag)
        {
            //Do Nothing
        }
        else if (colliderTag == "Player")
        {
            PlayerController c = OtherCollider.gameObject.GetComponent<PlayerController>();
            //Take Damage
        }
        else if (colliderTag == "Enemy")
        {
            Enemy e = OtherCollider.gameObject.GetComponent<Enemy>();
            e.TakeDamage(damage);
        }
        //Check layer of other collider.
        //Ensure that bullet isn't hitting the shooter

    }
}
