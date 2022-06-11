using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("HUD")]
    [SerializeField] private Text coinText, livesText;
    [SerializeField] private GameObject pausePanel;

    [Header("Win")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text playerScore, livesLeft, finalScore;

    [Header("Lose")]
    [SerializeField] private GameObject losePanel;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateScore()
    {
        coinText.text = "score: " + GameManager.instance.score.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = "lives: " + GameManager.instance.lives.ToString();
    }

    public void Win()
    {

        playerScore.text = "Score: " + GameManager.instance.score;
        livesLeft.text = "Lives Left: " + GameManager.instance.lives;
        int calculatedScore = GameManager.instance.score + (GameManager.instance.lives * 100);
        finalScore.text = "Final Score: " + calculatedScore;
        StartCoroutine(WinCo());
    }

    IEnumerator WinCo()
    {
        winPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        livesLeft.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        finalScore.gameObject.SetActive(true);
    }

    public void Lose()
    {
        StartCoroutine(LoseCo());
    }

    IEnumerator LoseCo()
    {
        losePanel.SetActive(true);
        yield break;
    }

    public void ShowPause()
    {
        pausePanel.SetActive(true);
    }

    public void HidePause()
    {
        pausePanel.SetActive(false);
    }
}
