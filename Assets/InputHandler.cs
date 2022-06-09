using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    private bool leftPressed, rightPressed;
    public bool isJump, isFire;

    public float dir;

    private void Awake()
    {
        instance = this;
    }

    public void RightButton_Down()
    {
        rightPressed = true;
    }

    public void RightButton_Up()
    {
        rightPressed = false;
    }

    public void LeftButton_Down()
    {
        leftPressed = true;
    }

    public void LeftButton_Up()
    {
        leftPressed = false;
    }

    public void JumpButton_Down()
    {
        isJump = true;
        
    }

    public void JumpButton_Up()
    {
        isJump = false;

    }
    public void FireButton_Down()
    {
        isFire = true;
    }

    public void FireButton_Up()
    {
        isFire = false;
    }


    private void Update()
    {
        if (rightPressed && leftPressed) // both directions
            dir = 0;
        else if (rightPressed && !leftPressed ) // only right
            dir = 1;
        else if (leftPressed && !rightPressed) // only left
            dir = -1; 
        else 
            dir = 0;
    }


}
