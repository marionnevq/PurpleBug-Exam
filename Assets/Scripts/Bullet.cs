using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject explosionEffect, shroom, heart;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Pickup")
        {
            if (other.tag == "Enemy")
            {
                PlayerController.instance.Bounce();
                AudioManager.instance.PlaySFX(6);
                Instantiate(explosionEffect, other.transform.position, other.transform.rotation);
                float dropSelect = Random.Range(0, 100f);

                if (other.gameObject.GetComponent<EnemyType>().type == EnemyType.Type.land)
                {
                    other.transform.parent.gameObject.GetComponent<LandEnemyController>().Kill();

                    if (dropSelect <= other.gameObject.GetComponent<EnemyType>().chanceToDrop)
                    {
                        Instantiate(shroom, other.transform.position, other.transform.rotation);

                    }
                }

                if (other.gameObject.GetComponent<EnemyType>().type == EnemyType.Type.flying)
                {
                    other.transform.parent.gameObject.GetComponent<FlyingEnemyController>().Kill();

                    if (dropSelect <= other.gameObject.GetComponent<EnemyType>().chanceToDrop)
                    {
                        Instantiate(heart, other.transform.position, other.transform.rotation);

                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
