using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;

    PlayerController player;
    //float xSpeed;
    Rigidbody2D myRigidBody;
    bool isTravelingUp;

    Vector2 Trajectory;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //xSpeed = transform.localScale.x * 10;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(Speed * Trajectory.x, Speed * Trajectory.y);
    }

    public void SetTrajectory(Vector2 trajectory)
    {
        Trajectory = trajectory;
        if (trajectory.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
