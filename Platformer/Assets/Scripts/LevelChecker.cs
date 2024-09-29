using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, UnfinishedText;
    [SerializeField] private int requierment;
    [SerializeField] private int levelToLoad;

    private bool levelIsLoading = false;
    private bool accepted = false;

    private void Update()
    {
        if (accepted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke("LoadNextLevel", 0f);
                levelIsLoading = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //kod som kollar om spelaren har möt kriteriet för att fortsätta
        if (other.CompareTag("Player"))
        {
            if(PlayerMovement.levelsCompleted >= requierment)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                accepted = true;
            }
            else
            {
                dialogueBox.SetActive(true);
                UnfinishedText.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        accepted = false;
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            UnfinishedText.SetActive(false);
        }
    }
}
