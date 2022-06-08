using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;

    private bool hasSpawned = false;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (!hasSpawned)
        {
            if (other.gameObject.tag == "Player" && other.transform.position.y < transform.position.y)
            {
                Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
            hasSpawned = true;
        }


    }
}
