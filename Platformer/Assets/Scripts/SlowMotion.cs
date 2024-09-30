using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float newGravity;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        other.GetComponent<Rigidbody2D>().gravityScale = newGravity;
    }
}
