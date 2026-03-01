using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    public AudioClip openClip; // assign door open sound in Inspector
    private AudioSource audioSource;

    private void Awake(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    [ContextMenu("Open")]
    public void Open() {
        animator.SetTrigger("Open");
        if (audioSource != null && openClip != null)
        {
            audioSource.PlayOneShot(openClip);
        }
    }
    
}
