using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{

    Animator myAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookUp(bool isShooting)
    {
        //Look Up frame in animator
        myAnimator.SetBool("isLookingUp",true);
        //If Shooting, go to ShootingFrame from Animator.
    }
    public void LookDown()
    {
        //Look Idle Frame in animator
        myAnimator.SetBool("isLookingUp", false);
    }
}
