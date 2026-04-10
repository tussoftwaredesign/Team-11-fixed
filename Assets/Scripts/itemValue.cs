using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemValue : MonoBehaviour
{
    public int pickUpValue;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
         void OnCollisionEnter2D(Collision2D other)
    {
        
        
        if (other.gameObject.CompareTag("Big Guy"))
        {
            
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Mid Guy"))
        {
            
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Small Guy"))
        {
            
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
