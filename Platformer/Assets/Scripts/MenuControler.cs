using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(PlayerMovement.levelsCompleted + 2);
    }

    public void HubLevel()
    {
        SceneManager.LoadScene(5);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
