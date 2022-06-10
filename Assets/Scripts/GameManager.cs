using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives;
    public int score;
    [SerializeField] private float waitToRespawn;
    [SerializeField] private Transform spawnPoint;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Time.timeScale = 1f;
        UIManager.instance.UpdateScore();
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

        if (lives <= 0)
        {
            //AudioManager.Lose();
            UIManager.instance.UpdateLives();
            UIManager.instance.UpdateScore();
            Time.timeScale = 0;
            UIManager.instance.Lose();
        }
        else
        {
            PlayerController.instance.gameObject.SetActive(true);
            PlayerController.instance.gameObject.transform.position = spawnPoint.position;
            UIManager.instance.UpdateLives();

        }

    }

    public void Endlevel()
    {

        PlayerController.instance.Win();
        PlayerController.instance.stopInput = true;
        StartCoroutine(EndLevelCo());

    }

    IEnumerator EndLevelCo()
    {
        //AudioManager.Win()
        UIManager.instance.UpdateLives();
        UIManager.instance.UpdateScore();
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        UIManager.instance.Win();

    }
}
