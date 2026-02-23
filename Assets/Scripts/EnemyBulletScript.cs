using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private float timer;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 6)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
        
        if (other.gameObject.CompareTag("Big Guy"))
        {
            other.gameObject.GetComponent<PlayerManager>().playerHealth -= 1;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Mid Guy"))
        {
            other.gameObject.GetComponent<PlayerManager>().playerHealth -= 1;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Small Guy"))
        {
            other.gameObject.GetComponent<PlayerManager>().playerHealth -= 1;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Collision"))
        {
            Destroy(gameObject);
        }
    }
}
