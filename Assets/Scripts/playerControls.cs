using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControls : MonoBehaviour
{
    public enum PlayerState { Form1, Form2, Form3 }

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GroundCheck groundCheck;

    private Sprite form1Sprite;
    private Sprite form2Sprite;
    private Sprite form3Sprite;

    public Vector2 movementDirection;
    public int movementSpeed = 5;
    public int jumpImpulse = 5;
    public PlayerState currentState = PlayerState.Form1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        groundCheck = gameObject.GetComponent<GroundCheck>();

        // Load sprites from Resources folder
        form1Sprite = Resources.Load<Sprite>("PlayerSprites/Player");
        form2Sprite = Resources.Load<Sprite>("PlayerSprites/Player (Small)");
        form3Sprite = Resources.Load<Sprite>("PlayerSprites/Player (Big)");

        // Debug logs to check if sprites loaded
        if (form1Sprite == null) Debug.LogError("Failed to load 'PlayerSprites/Player' sprite. Ensure it's in Assets/Resources/PlayerSprites/ and named 'Player'.");
        if (form2Sprite == null) Debug.LogError("Failed to load 'PlayerSprites/Player (Small)' sprite. Ensure it's in Assets/Resources/PlayerSprites/ and named 'Player (Small)'.");
        if (form3Sprite == null) Debug.LogError("Failed to load 'PlayerSprites/Player (Big)' sprite. Ensure it's in Assets/Resources/PlayerSprites/ and named 'Player (Big)'.");
    }

    // Update is called once per frame
    void Update()
    {
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
            // Is the Walking animation set the False?
            if (animator.GetBool("walking") == false)
            {
                // Enable the Walking Animation
                animator.SetBool("walking", true);
            }

            if (movementDirection.x < 0) { spriteRenderer.flipX = true; }
            else {
                spriteRenderer.flipX = false;
            }
        }
        // If the player is not moving - they're standing still
        else {
            // Disable the Walking Animation
            animator.SetBool("walking", false);
        }
    }

    void UpdateFormProperties()
    {
        switch (currentState)
        {
            case PlayerState.Form1:
                movementSpeed = 10;
                jumpImpulse = 8;
                spriteRenderer.sprite = form2Sprite;
                break;
            case PlayerState.Form2:
                movementSpeed = 5;
                jumpImpulse = 5;
                spriteRenderer.sprite = form1Sprite;
                break;
            case PlayerState.Form3:
                movementSpeed = 2;
                jumpImpulse = 1;
                spriteRenderer.sprite = form3Sprite;
                break;
        }
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
        animator.SetTrigger("attack");
    }

    void OnJump() {
        // Trigger the Jump Animation
        if (groundCheck.isGrounded)
        {
            Debug.Log("Player has Jumped!");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            animator.SetTrigger("Jump");
        }
    }
}
