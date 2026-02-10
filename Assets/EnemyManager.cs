using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    Animator animator;
    ItemHealth itemHealth;

    bool deathTriggered = false;
    public int destroyTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        itemHealth = gameObject.GetComponent<ItemHealth>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if (itemHealth.health <= 0 && !deathTriggered){
            animator.SetTrigger("enemy_death");

            deathTriggered = true;

            StartCoroutine(DestroyGameObject(destroyTimer));
        }
    }
     void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
        
        if (other.gameObject.CompareTag("Big Guy"))
       {
            
            Destroy(gameObject);
            Debug.Log("Squished Enemy!");
        }
    }
    IEnumerator DestroyGameObject(int delayTime){

        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
