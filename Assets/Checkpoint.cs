﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer theSR;
    [SerializeField] private Sprite cpOn, cpOff;
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.DeactivateCheckpoint();

            theSR.sprite = cpOn;

            CheckpointManager.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
}
