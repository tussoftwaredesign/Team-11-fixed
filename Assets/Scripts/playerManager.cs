using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerState { Form1, Form2, Form3 }

    public GameObject form1Prefab; // Assign the small player prefab
    public GameObject form2Prefab; // Assign the normal player prefab
    public GameObject form3Prefab; // Assign the big player prefab

    private Transform currentPlayer;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GroundCheck groundCheck;

    public Vector2 movementDirection;
    public int movementSpeed = 5;
    public int jumpImpulse = 5;
    public float timer = 2.0f;
    public float playerHealth = 12;
    public float playerMaxHealth;
    private bool isDead = false;
    public PlayerState currentState = PlayerState.Form1;
    public GameObject swapEffect;
    public AudioClip collectClip; // Assign collectable pickup sound in Inspector
    public AudioClip jumpClip; // Assign jump sound in Inspector
    public AudioClip punchClip; // Assign punch sound in Inspector
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 10;
        playerMaxHealth = playerHealth;

        if (form2Prefab == null)
        {
            Debug.LogError("form2Prefab not assigned in Inspector!");
            return;
        }
        // Instantiate the initial form (Form1 - Small)
        currentPlayer = Instantiate(form2Prefab, transform.position, transform.rotation).transform;
        GetComponentsFromCurrentPlayer();
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void GetComponentsFromCurrentPlayer()
    {
        rb = currentPlayer.GetComponent<Rigidbody2D>();
        // animator lives on the player prefab itself, not on the manager object
        animator = currentPlayer.GetComponent<Animator>();
        spriteRenderer = currentPlayer.GetComponent<SpriteRenderer>();
        groundCheck = currentPlayer.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // animator reference should already point at currentPlayer; no need to re-fetch every frame
        if (playerHealth <= 0 && !isDead)
        {
           Debug.Log("Death triggered");
            isDead = true;
            movementDirection = Vector2.zero;
            StartCoroutine(ReloadScene());
        }

        // Is the Player Moving?

        if (movementDirection != Vector2.zero)
        {
            if (animator.GetBool("Walking") == false)
            {
                // Enable the Run Animation
                animator.SetBool("Walking", true);
                

            }


            if (movementDirection.x < 0) { spriteRenderer.flipX = true; }
            else {
                spriteRenderer.flipX = false;
            }
        }
        // If the player is not moving - they're standing still
           else {
            // Disable the Run Animation
            animator.SetBool("Walking", false);
            
        }

        if (groundCheck.isGrounded)
        {
            animator.SetBool("Falling", false);
        }
        else
        {
            animator.SetBool("Falling", true);
        }

    }
        void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
            Debug.Log("PlayerHealth" + playerHealth);
            animator.SetTrigger("Swap");
        }
        if (other.gameObject.tag == "Projectile")
        {    
            playerHealth -= 1;
            Debug.Log("PlayerHealth" + playerHealth);
            animator.SetTrigger("Swap");
        }
        if (other.gameObject.tag == "Collectable") {


            GameObject collectable = other.gameObject;

            int collectableValue = collectable.GetComponent<itemValue>().pickUpValue;

            playerHealth += collectableValue;
            
            // play pickup sound if assigned
            if (audioSource != null && collectClip != null)
            {
                audioSource.PlayOneShot(collectClip);
            }

            Destroy(collectable);

            Debug.Log("Health: " + playerHealth);
        }
    }
    void UpdateFormProperties()
    {
        // Save current position and velocity
        Vector3 currentPos = currentPlayer.position;
        Vector2 currentVel = rb.linearVelocity;

        // Destroy current player
        Destroy(currentPlayer.gameObject);

        // Instantiate new form
        GameObject newPlayerPrefab = null;
        switch (currentState)
        {
            case PlayerState.Form1:
                newPlayerPrefab = form2Prefab; // Small
                movementSpeed = 10;
                
                jumpImpulse = 21;
                break;
            case PlayerState.Form2:
                newPlayerPrefab = form1Prefab; // Normal
                movementSpeed = 6;
                
                jumpImpulse = 10;
                break;
            case PlayerState.Form3:
                newPlayerPrefab = form3Prefab; // Big
                movementSpeed = 3;
                
                jumpImpulse = 0;
                break;
        }

        if (newPlayerPrefab == null)
        {
            Debug.LogError("Prefab not assigned for state " + currentState);
            return;
        }

        currentPlayer = Instantiate(newPlayerPrefab, currentPos, Quaternion.identity).transform;
        GetComponentsFromCurrentPlayer();
        rb.linearVelocity = currentVel; // Restore velocity
        // fire swap trigger on new animator so the animation actually plays
        animator.SetTrigger("Swap");
    }

    void FixedUpdate()
    {
        // Move the Player on the X axis only
        rb.linearVelocity = new Vector2((movementDirection.x * movementSpeed), rb.linearVelocity.y);
    }

    void OnMove(InputValue movementValue)
    {
        // When user input is detected - update the movement variable
        movementDirection = movementValue.Get<Vector2>();
    }

    void OnFire() {
        // Trigger the Attack Animation
        animator.SetTrigger("Attack");
        // play punch sound if assigned
        if (audioSource != null && punchClip != null)
        {
            audioSource.PlayOneShot(punchClip);
        }
    }

    void OnSmall() {
        if (currentState != PlayerState.Form1) {
            currentState = PlayerState.Form1;
            Instantiate(swapEffect, currentPlayer.transform.position, currentPlayer.transform.rotation);
            UpdateFormProperties();  
        }
        
    }
    void OnMedium() {
        if (currentState != PlayerState.Form2) {
            currentState = PlayerState.Form2;
            Instantiate(swapEffect, currentPlayer.transform.position, currentPlayer.transform.rotation);
            UpdateFormProperties();
        }
        
        
    }
    void OnBig() {
        if (currentState != PlayerState.Form3) {
            currentState = PlayerState.Form3;
            Instantiate(swapEffect, currentPlayer.transform.position, currentPlayer.transform.rotation);
            UpdateFormProperties();
        }
        
    }

    void OnJump() {
        // Trigger the Jump Animation
        if (groundCheck.isGrounded)
        {
           
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
            // play jump sound if assigned
            if (audioSource != null && jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }
        }
    }
        private IEnumerator ReloadScene()
    {
        Debug.Log("Reloading scene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collectable") {
            
        }
    }
}
