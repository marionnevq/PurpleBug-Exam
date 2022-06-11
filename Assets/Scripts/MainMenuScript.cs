using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] public GameObject levelSelect, leaderboards;
    public void Play(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void OpenLevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelect.SetActive(false);
    }
    public void OpenLeaderBoards()
    {
        leaderboards.SetActive(true);
    }
    public void CloseLeaderBoards()
    {
        leaderboards.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
