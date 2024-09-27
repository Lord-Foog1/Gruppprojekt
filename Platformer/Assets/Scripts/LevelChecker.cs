using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, UnfinishedText;
    [SerializeField] private int requierment;
    [SerializeField] private int levelToLoad;

    private Animator anim;
    private bool levelIsLoading = false;
    private bool levelIsComplete = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //kod som kollar om spelaren har möt kriteriet för att fortsätta
        if (other.CompareTag("Player"))
        {
            if(PlayerMovement.levelsCompleted >= requierment)
            {
                //Kod för att spela animationen till dörren
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Flag");
                    Invoke("LoadNextLevel", 4.0f);
                    levelIsLoading = true;
                }       
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
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            UnfinishedText.SetActive(false);
        }
    }
}
