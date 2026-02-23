using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int shards = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collectable") {
            //Debug.Log("Collided with: " + collision.collider.name);

            GameObject collectable = collision.gameObject;

            int collectableValue = collectable.GetComponent<itemValue>().pickUpValue;

            shards += collectableValue;
            
            Destroy(collectable);

            Debug.Log("Shards: " + shards);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collectable") {
            Debug.Log("Collision ended with: " + collision.collider.name);
        }
    }
}
