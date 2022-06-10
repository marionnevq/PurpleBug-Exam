using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private SpriteRenderer theSR;
    private bool isGrown;

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

    public bool stopInput;

    public float inputX;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        isGrown = false;
        stopInput = false;
        theRB.bodyType = RigidbodyType2D.Dynamic;


    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            if (!stopInput)
            {
                //Movement
                theRB.velocity = new Vector2(moveSpeed * inputX, theRB.velocity.y);
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (theRB.velocity.x < 0 && facingRight)
                {
                    Flip();
                }
                else if (theRB.velocity.x > 0 && !facingRight)
                {
                    Flip();
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

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //AudioManager.instance.PlaySFX(3);
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

            }
            else
            {
                //AudioManager.instance.PlaySFX(3);
                if (canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

                    canDoubleJump = false;
                }

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

    public void Fire(InputAction.CallbackContext context)
    {
        if (ammo > 0 && context.performed)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ammo--;
        }
    }

    public void Grow()
    {
        transform.localScale = new Vector3(5f, 10f, 0f);
        isGrown = true;
    }

    public void Shrink()
    {
        transform.localScale = new Vector3(5f, 5f, 0f);

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

    public void Win()
    {
        theRB.velocity = Vector2.zero;
        theRB.bodyType = RigidbodyType2D.Static;
    }
}
