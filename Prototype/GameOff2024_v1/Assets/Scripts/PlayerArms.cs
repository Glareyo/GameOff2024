using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    [SerializeField] GameObject Bullet;

    Animator myAnimator;
    bool IsHoldingPistol;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        IsHoldingPistol = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void Attack()
    {
        if (IsHoldingPistol)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            Instantiate(Bullet,transform.position, Quaternion.identity);
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