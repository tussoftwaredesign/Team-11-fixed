using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundCheck : MonoBehaviour
{

    CapsuleCollider2D capsuleCollider2D;
    public ContactFilter2D cast;
    public float groundDistance = 0.05F;
    RaycastHit2D[] raycastHit2D = new RaycastHit2D[5];
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Determine if we're colliding with anything 
        int collisions = capsuleCollider2D.Cast(Vector2.down, cast, raycastHit2D, groundDistance);
        
        // If we collide with the ground
        if (collisions > 0)
        {
            isGrounded = true;
            
        }
        // if we're not colliding with the ground (jumping)
        else 
        {
            isGrounded = false;
        }
    }
}