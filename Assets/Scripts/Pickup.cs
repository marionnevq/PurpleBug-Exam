using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public enum Type {
        coin,
        heart,
        ammo,
        star,
        shroom
    }

    [SerializeField] private Type pickupType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            switch(pickupType)
            {
                case Type.coin:
                    GameManager.instance.score+= 100;
                    UIManager.instance.UpdateScore();
                    Destroy(gameObject);
                    break;

                case Type.heart:
                    GameManager.instance.lives++;
                    UIManager.instance.UpdateLives();
                    Destroy(gameObject);
                    break;

                case Type.ammo:
                    PlayerController.instance.ammo++;
                    Destroy(gameObject);
                    break;

                case Type.star:

                    break;

                case Type.shroom:
                    PlayerController.instance.Grow();
                    Destroy(gameObject);
                    break;
            }
        }    
    }
}
