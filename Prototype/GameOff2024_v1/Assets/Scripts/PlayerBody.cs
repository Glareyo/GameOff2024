using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    Animator myAnimator;

    string JumpBool = "IsJumping";
    string FallingBool = "IsFalling";
    string RunningBool = "IsRunning";

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Run()
    {
        myAnimator.SetBool(RunningBool, true);
    }
    public void Jump()
    {
        myAnimator.SetBool(JumpBool, true);
    }
    public void Land()
    {

    }
    public void Idle()
    {
        myAnimator.SetBool(JumpBool, false);
        myAnimator.SetBool(FallingBool, false);
        myAnimator.SetBool(RunningBool, false);
    }
    public void Fall()
    {
        myAnimator.SetBool(JumpBool, false);
        myAnimator.SetBool(FallingBool, true);
    }


    
}
