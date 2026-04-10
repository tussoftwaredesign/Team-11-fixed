using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    Animator animator;
    ItemHealth itemHealth;

    bool deathTriggered = false;
    public int destroyTimer = 1;
    public GameObject explosionEffect;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
        itemHealth = gameObject.GetComponent<ItemHealth>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
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
        
        
        if (other.gameObject.CompareTag("Big Guy"))
       {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
    }
    IEnumerator DestroyGameObject(int delayTime){

        yield return new WaitForSeconds(delayTime);
        Instantiate(explosionEffect, transform.position, transform.rotation);    
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Destroy(gameObject);
    }
}
