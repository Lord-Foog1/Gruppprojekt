using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, UnfinishedText;
    [SerializeField] private int questGoal = 20;
    [SerializeField] private int levelToLoad;
    [SerializeField] private int levelDone;

    private Animator anim;
    private bool levelIsLoading = false;
    private bool levelIsComplete = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().collected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                if(!levelIsComplete)
                {
                    PlayerMovement.levelsCompleted = levelDone;
                    levelIsComplete = true;
                }
                anim.SetTrigger("Flag");
                Invoke("LoadNextLevel", 4.0f);
                levelIsLoading = true;
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
