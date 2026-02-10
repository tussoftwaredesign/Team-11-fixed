using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroZone : MonoBehaviour
{

    public GameObject target;
    public GameObject enemy;
    public float movementSpeed;

    private Rigidbody2D enemyRigidBody;
    private Vector2 calculatedDirection;

    private Vector2 calculatedDistance;
    private bool targetDetected = false;

    private Animator enemyAnimator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();

        enemyAnimator = enemy.GetComponent<Animator>();

        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDetected)
        {

            if (calculatedDirection.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else { 
                spriteRenderer.flipX = true;
            }
        }
        else {

        } 


    }

    private void FixedUpdate()
    {
        if (targetDetected)
        {
            // Calculate the Direction to Run
            calculatedDirection = (target.transform.position - enemy.transform.position).normalized;
            
            // Calculate the Distance from the player
            calculatedDistance = target.transform.position - enemy.transform.position;

            // Check the Distance is greater than 2
            if (Mathf.Abs(calculatedDistance.x) >= 2)
            {
                //Debug.Log(calculatedDirection + " --- " + calculatedDistance + " --- " + Mathf.Abs(calculatedDistance.x));
                
                // Move to the Players Position
                enemyRigidBody.velocity = new Vector2(calculatedDirection.x * movementSpeed, enemyRigidBody.velocity.y);



            }
            // Stop moving if we're too close
            else
            {
                enemyRigidBody.velocity = new Vector2(0, 0);
            }
        }
        // Stop moving
        else
        {
            enemyRigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collision object has one of the target tags
        if (collision.CompareTag("Big Guy") || collision.CompareTag("Mid Guy") || collision.CompareTag("Small Guy"))
        {
            target = collision.gameObject;
            // Debug.Log("Enter: " + collision.name);
            targetDetected = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if exiting object was the current target
        if (target != null && collision.gameObject == target)
        {
            //Debug.Log("Exit: " + collision.name);
            targetDetected = false;
            target = null;
        }
    }



}