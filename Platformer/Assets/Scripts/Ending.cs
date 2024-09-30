using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private int levelToLoad;

    private bool levelIsComplete = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("LoadNextLevel", 0.5f);
            other.GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
