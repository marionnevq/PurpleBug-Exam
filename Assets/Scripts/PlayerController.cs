﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public SpriteRenderer theSR;
    [SerializeField] private bool isGrown;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D theRB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float bounceForce;
    private bool isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;
    private bool canDoubleJump;
    private bool facingRight = true;


    [Header("Firing")]
    public int ammo;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Damage")]
    public float knockBackLength, knockBackForce, invincibleLength;
    private float knockBackCounter;
    private float invincibleCounter;

    

    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        isGrown = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            //Movement
            theRB.velocity = new Vector2(moveSpeed * InputHandler.instance.dir, theRB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (InputHandler.instance.isJump)
            {
                InputHandler.instance.isJump = false;
                if (isGrounded)
                {
                    //AudioManager.instance.PlaySFX(3);

                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        //AudioManager.instance.PlaySFX(3);

                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

                        canDoubleJump = false;
                    }
                }
            }

            if (theRB.velocity.x < 0 && facingRight)
            {
                Flip();
            }
            else if (theRB.velocity.x > 0 && !facingRight)
            {
                Flip();
            }

            if (InputHandler.instance.isFire)
            {
                if (ammo > 0)
                {
                    Fire();
                    ammo--;
                }
            }

        }
        else
        {
            knockBackCounter -= Time.deltaTime;

            if (facingRight)
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);

            }
            else
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
        }

        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b);
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
    public void DamagePlayer()
    {
        if (invincibleCounter <= 0)
        {
            if (isGrown)
            {
                Shrink();
                invincibleCounter = invincibleLength;
                StartCoroutine(Flash());
                KnockBack();
            }
            else
            {
                KillPlayer();
            }
        }
    }
    public void KillPlayer()
    {
        GameManager.instance.RespawnPlayer();
    }

    private void Fire()
    {
        InputHandler.instance.isFire = false;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Grow()
    {
        transform.localScale = new Vector3(5f,10f,0f);
        isGrown = true;
    }

    public void Shrink()
    {
        transform.localScale = new Vector3(5f,5f,0f);

        isGrown = false;
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);

    }

    private IEnumerator Flash()
    {
            Color og = theSR.color;
            theSR.color = new Color(255f, 255f, 255f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            theSR.color = new Color(og.r, og.g, og.b, 0.5f);

        
    }

    
}