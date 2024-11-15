using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    [SerializeField] GameObject Bullet;


    GameObject PlayerController;
    Animator myAnimator;
    bool IsHoldingPistol;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        PlayerController = GameObject.Find("Player");
        IsHoldingPistol = false;
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
        if (IsHoldingPistol)
        {
            GameObject b = Instantiate(Bullet, transform.position, transform.rotation);
            Bullet bullet = b.GetComponent<Bullet>();
            if (lookingUp)
            {
                bullet.SetTrajectory(new Vector2(0, 1));
            }
            else
            {
                bullet.SetTrajectory(new Vector2(PlayerController.transform.localScale.x, 0));
            }
            //(b as Bullet).SetTrajectory(PlayerController.transform.localScale, false);

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