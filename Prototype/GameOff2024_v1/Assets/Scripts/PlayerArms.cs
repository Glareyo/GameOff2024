using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{

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

    public void HoldingPistol()
    {
        myAnimator.SetBool("IsHoldingPistol", true);
        IsHoldingPistol = true;
    }
    
    public void Unarmed()
    {
        myAnimator.SetBool("IsHoldingPistol", false);
        IsHoldingPistol = false;
    }
}
