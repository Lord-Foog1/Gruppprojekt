using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip pickUpSound, healthPickUp;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private GameObject bananaParticles, melonParticles, dustParticles;
    [SerializeField] private TMP_Text collectableText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color goodHealth, badHealth;
    private float horizontalValue;
    private float rayDistance = 0.25f;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int collected = 0;
    private bool isGrounded;
    private bool canMove = true;
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        collectableText.text = "" + collected;
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0)
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
    }

    private void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            collected++;
            collectableText.text = "" + collected;
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.PlayOneShot(pickUpSound, 0.3f);
            Instantiate(bananaParticles, other.transform.position, Quaternion.identity);
        }

        if(other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        audioSource.pitch = 1;
        int randomValue = Random.Range(0,jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomValue], 0.3f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealth();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void TakeKnockback(float KnockbackForce, float Upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(KnockbackForce, Upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void UpdateHealth()
    {
        healthSlider.value = currentHealth;

        if(currentHealth >= 2)
        {
            fillColor.color = goodHealth;
        }
        else
        {
            fillColor.color = badHealth;
        }
    }

    private void RestoreHealth(GameObject HealthPickup)
    {
        if(currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int HealthToRestore = HealthPickup.GetComponent<HealthPickup>().healthAmount;
            currentHealth += HealthToRestore;
            audioSource.pitch = 1;
            audioSource.PlayOneShot(healthPickUp, 0.3f);
            UpdateHealth();
            Destroy(HealthPickup);
            Instantiate(melonParticles, HealthPickup.GetComponent<HealthPickup>().transform.position, Quaternion.identity);

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth; 
            }
        }
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealth();
        transform.position = spawnPosition.position;
        rgbd.velocity = Vector2.zero;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
