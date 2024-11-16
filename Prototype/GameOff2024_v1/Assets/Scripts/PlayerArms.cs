using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    [SerializeField] GameObject Bullet;


    GameObject PlayerController;
    Animator myAnimator;
    bool IsHoldingPistol;
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        PlayerController = GameObject.Find("Player");
        IsHoldingPistol = false;
        damage = PlayerController.GetComponent<PlayerController>().GetDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LookingDown()
    {
        myAnimator.SetBool("IsLookingUp", false);
    }
    public void LookingUp()
    {
        myAnimator.SetBool("IsLookingUp", true);
    }

    //Swap Weapons Animation
    public void SwapWeapons()
    {
        if (IsHoldingPistol)
        {
            Unarmed();
        }
        else
        {
            HoldingPistol();
        }
    }

    //Attack
    public void Attack(bool lookingUp)
    {
        //Determine if player is holding the pistol
        if (IsHoldingPistol)
        {
            //Create a gameobject of the bullet
            GameObject b = Instantiate(Bullet, transform.position, transform.rotation);
            //Get the script of the fired bullet
            Bullet bullet = b.GetComponent<Bullet>();
            //Trajectory of the bullet
            Vector2 trajectory = new Vector2();

            //If looking up, set bullet's trajectory to go up.
            if (lookingUp)
            {
                trajectory = new Vector2(0, 1);
            }
            else //Else set bullet trajectory to go left or right
            {
                trajectory = new Vector2(PlayerController.transform.localScale.x, 0);
            }

            bullet.SetBullet(trajectory,damage,this.tag);

            //Animate
            myAnimator.SetTrigger("Shoot");
        }
    }

    //Hold the Pistol
    public void HoldingPistol()
    {
        myAnimator.SetBool("IsHoldingPistol", true);
        IsHoldingPistol = true;
    }
    
    //Go to Unarmed
    public void Unarmed()
    {
        myAnimator.SetBool("IsHoldingPistol", false);
        IsHoldingPistol = false;
    }
}