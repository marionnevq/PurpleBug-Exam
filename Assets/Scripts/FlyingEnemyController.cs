using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed;
    private int currentPoint;

    [SerializeField] private SpriteRenderer theSr;

    [SerializeField] private float distanceToAttackPlayer, chaseSpeed;
    private Vector3 attacktarget;

    [SerializeField] private float waitAfterAttack;
    private float attackCounter;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;

        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                attacktarget = Vector3.zero;


                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }

                if (transform.position.x < points[currentPoint].position.x)
                {
                    theSr.flipX = false;
                }
                else
                {
                    theSr.flipX = true;
                }
            }
            else
            {

                if (attacktarget == Vector3.zero)
                {
                    attacktarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attacktarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attacktarget) <= 0.1f)
                {
                    attackCounter = waitAfterAttack;

                    attacktarget = Vector3.zero;
                }
            }
        }
    }

    public void Kill()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i].gameObject);
        }
        Destroy(this.gameObject);
    }
}
