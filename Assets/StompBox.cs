using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    [SerializeField] private GameObject shroom, heart;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            PlayerController.instance.Bounce();



            //AudioManager.instance.PlaySFX(0);
            //Instantiate(explosionEffect, other.transform.position, other.transform.rotation);

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
    }
}
