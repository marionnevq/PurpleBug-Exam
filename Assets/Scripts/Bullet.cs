﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform. right *speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player" && other.tag != "Pickup")
        {
            Destroy(gameObject);
        }
    }
}
