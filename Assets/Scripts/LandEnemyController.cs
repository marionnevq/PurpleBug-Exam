using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemyController : MonoBehaviour
{
    Rigidbody2D theRb;
    SpriteRenderer theSr;
    [SerializeField] private Transform leftPoint, rightPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveToRight;
    [SerializeField] private float moveTime, waitTime;
    private float moveCount, waitCount;

    [SerializeField] private GameObject collectible;
    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();
        theSr = GetComponentInChildren<SpriteRenderer>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        moveToRight = true;
        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            //anim.SetBool("isMoving", true);

            if (moveToRight)
            {
                theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
                theSr.flipX = false;

                if (transform.position.x > rightPoint.position.x)
                {
                    moveToRight = false;
                }
            }
            else
            {
                theRb.velocity = new Vector2(-moveSpeed, theRb.velocity.y);
                theSr.flipX = true;

                if (transform.position.x < leftPoint.position.x)
                {
                    moveToRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            // anim.SetBool("isMoving", false);
            theRb.velocity = new Vector2(0f, theRb.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
            }
        }
    }

    public void Kill()
    {
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        Destroy(this.gameObject);

    }
}
