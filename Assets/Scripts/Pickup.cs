using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public enum Type
    {
        coin,
        heart,
        ammo,
        star,
        shroom
    }

    [SerializeField] private Type pickupType;
    [SerializeField] private GameObject pickupEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            switch (pickupType)
            {
                case Type.coin:
                    AudioManager.instance.PlaySFX(4);
                    GameManager.instance.score += 100;
                    UIManager.instance.UpdateScore();
                    Destroy(gameObject);
                    break;

                case Type.heart:
                    AudioManager.instance.PlaySFX(5);
                    GameManager.instance.lives++;
                    GameManager.instance.score += 150;
                    UIManager.instance.UpdateLives();
                    Destroy(gameObject);
                    break;

                case Type.ammo:
                    AudioManager.instance.PlaySFX(4);
                    PlayerController.instance.ammo++;
                    UIManager.instance.UpdateAmmo();
                    Destroy(gameObject);
                    break;

                case Type.star:

                    break;

                case Type.shroom:
                    AudioManager.instance.PlaySFX(5);
                    GameManager.instance.score += 200;
                    PlayerController.instance.Grow();
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
