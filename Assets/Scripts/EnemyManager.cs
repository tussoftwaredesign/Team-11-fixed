using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    Animator animator;
    ItemHealth itemHealth;
    public AudioSource flying;
    public AudioClip flyingClip; // assign looped flying sound in Inspector
    public AudioClip deathClip; // assign death sound in Inspector

    bool deathTriggered = false;
    public int destroyTimer = 1;
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        itemHealth = gameObject.GetComponent<ItemHealth>();
        flying = gameObject.GetComponent<AudioSource>();
        if (flying == null)
        {
            flying = gameObject.AddComponent<AudioSource>();
            flying.playOnAwake = false;
        }

        if (flying != null && flyingClip != null)
        {
            flying.clip = flyingClip;
            flying.loop = true;
            flying.Play();
        }
        flying = gameObject.GetComponent<AudioSource>();
        if (flying == null)
        {
            flying = gameObject.AddComponent<AudioSource>();
            flying.playOnAwake = false;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if (itemHealth.health <= 0 && !deathTriggered){
            // stop flying loop and play death sound
            if (flying != null)
            {
                if (flying.isPlaying) flying.Stop();
                if (deathClip != null) flying.PlayOneShot(deathClip);
            }

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
        Destroy(gameObject);
    }
}
