using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("HUD")]
    public Text coinText, livesText;
    public GameObject pausePanel;

    [Header("Win")]

    public GameObject winPanel;

    public Text playerScore, livesLeft, finalScore;

    [Header("Lose")]

    public GameObject losePanel;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateScore()
    {
        coinText.text = GameManager.instance.score.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = GameManager.instance.lives.ToString();
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
}
