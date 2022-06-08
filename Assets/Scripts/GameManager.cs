using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives;
    public int coins;
    [SerializeField] private float waitToRespawn;
    [SerializeField] private Transform spawnPoint;


    private void Awake() 
    {
        instance = this;     
    }

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        UIManager.instance.UpdateCoins();
        UIManager.instance.UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        lives--;
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);

        if(lives <= 0)
        {
            //end game
        } else 
        {
            PlayerController.instance.gameObject.SetActive(true);
            PlayerController.instance.gameObject.transform.position = spawnPoint.position;
            //PlayerHealthController.instance.ResetHealth();
            UIManager.instance.UpdateLives();
            
        }

    }
}
