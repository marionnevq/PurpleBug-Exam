using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives;
    public int score;
    [SerializeField] private float waitToRespawn;

    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private string nextScene;


    private bool isPaused;

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
        UIManager.instance.UpdateAmmo();
        isPaused = false;
        AudioManager.instance.PlayBGM();
    }

    public void RespawnPlayer()
    {
        lives--;
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.inputX = 0;
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);

        if (lives <= 0)
        {
            AudioManager.instance.StopBGM();
            UIManager.instance.UpdateLives();
            UIManager.instance.UpdateScore();
            Time.timeScale = 0;
            UIManager.instance.Lose();
        }
        else
        {
            PlayerController.instance.gameObject.SetActive(true);
            Vector3 delta = PlayerController.instance.gameObject.transform.position - CheckpointManager.instance.spawnPoint;
            vcam.OnTargetObjectWarped(PlayerController.instance.transform, delta);
            PlayerController.instance.gameObject.transform.position = CheckpointManager.instance.spawnPoint;
            UIManager.instance.UpdateLives();

        }

    }

    public void Endlevel(int score)
    {
        PlayerController.instance.Win();
        PlayerController.instance.stopInput = true;
        StartCoroutine(EndLevelCo());

    }

    IEnumerator EndLevelCo()
    {
        AudioManager.instance.StopBGM();
        UIManager.instance.UpdateLives();
        UIManager.instance.UpdateScore();
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        UIManager.instance.Win();

    }

    public void PauseGame(){

        
        if(!isPaused){
            isPaused = true;
            Time.timeScale = 0f;
            UIManager.instance.ShowPause();
        } else {
            isPaused = false;
            Time.timeScale = 1f;
            UIManager.instance.HidePause();
        }
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
