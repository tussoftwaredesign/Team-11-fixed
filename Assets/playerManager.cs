using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public int playerHealth = 12;
    private bool isDead = false;
    public PlayerState currentState = PlayerState.Form1;

    // Start is called before the first frame update
    void Start()
    {
        if (form2Prefab == null)
        {
            Debug.LogError("form2Prefab not assigned in Inspector!");
            return;
        }
        // Instantiate the initial form (Form1 - Small)
        currentPlayer = Instantiate(form2Prefab, transform.position, transform.rotation).transform;
        GetComponentsFromCurrentPlayer();
    }

    void GetComponentsFromCurrentPlayer()
    {
        rb = currentPlayer.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = currentPlayer.GetComponent<SpriteRenderer>();
        groundCheck = currentPlayer.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        animator = gameObject.GetComponent<Animator>();
        if (playerHealth <= 0 && !isDead)
        {
           Debug.Log("Death triggered");
            isDead = true;
            movementDirection = Vector2.zero;
            StartCoroutine(ReloadScene());
        }
        // Handle form switching
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentState = PlayerState.Form1;
            UpdateFormProperties();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentState = PlayerState.Form2;
            UpdateFormProperties();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = PlayerState.Form3;
            UpdateFormProperties();
        }

        // Is the Player Moving?

        if (movementDirection != Vector2.zero)
        {
            animator.SetBool("Walking", true);


            if (movementDirection.x < 0) { spriteRenderer.flipX = true; }
            else {
                spriteRenderer.flipX = false;
            }
        }
        // If the player is not moving - they're standing still
        else {
            // Disable the Walking Animation
            animator.SetBool("Walking", false);
        }
    }
        void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            playerHealth -= 1;
            Debug.Log("PlayerHealth" + playerHealth);
        }
        if (other.gameObject.tag == "Projectile")
        {    
            playerHealth -= 1;
            Debug.Log("PlayerHealth" + playerHealth);
        }
        if (other.gameObject.tag == "Collectable") {


            GameObject collectable = other.gameObject;

            int collectableValue = collectable.GetComponent<itemValue>().pickUpValue;

            playerHealth += collectableValue;
            
            Destroy(collectable);

            Debug.Log("Health: " + playerHealth);
        }
    }
    void UpdateFormProperties()
    {
        // Save current position and velocity
        Vector3 currentPos = currentPlayer.position;
        Vector2 currentVel = rb.velocity;

        // Destroy current player
        Destroy(currentPlayer.gameObject);

        // Instantiate new form
        GameObject newPlayerPrefab = null;
        switch (currentState)
        {
            case PlayerState.Form1:
                newPlayerPrefab = form2Prefab; // Small
                movementSpeed = 10;
                playerHealth = 1;
                jumpImpulse = 16;
                break;
            case PlayerState.Form2:
                newPlayerPrefab = form1Prefab; // Normal
                movementSpeed = 6;
                playerHealth = 5;
                jumpImpulse = 10;
                break;
            case PlayerState.Form3:
                newPlayerPrefab = form3Prefab; // Big
                movementSpeed = 3;
                playerHealth = 10;
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
        rb.velocity = currentVel; // Restore velocity
    }

    void FixedUpdate()
    {
        // Move the Player on the X axis only
        rb.velocity = new Vector2((movementDirection.x * movementSpeed), rb.velocity.y);
    }

    void OnMove(InputValue movementValue)
    {
        // When user input is detected - update the movement variable
        movementDirection = movementValue.Get<Vector2>();
    }

    void OnFire() {
        // Trigger the Attack Animation
        animator.SetTrigger("Attack");
    }

    void OnJump() {
        // Trigger the Jump Animation
        if (groundCheck.isGrounded)
        {
            Debug.Log("Player has Jumped!");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
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
            Debug.Log("Collision ended with: " + collision.collider.name);
        }
    }
}
