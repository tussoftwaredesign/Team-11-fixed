using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource WalkingAudio;
    public AudioSource AttackingAudio;
    public AudioSource JumpingAudio;
    public AudioSource DeathAudio;
    public AudioSource SwapAudio;
    public AudioSource PickupAudio;
    // Start is called before the first frame update

    public void PlayAttackingAudio()
    {
        Debug.Log("PlayAttackingAudio called");
        if (AttackingAudio != null && AttackingAudio.clip != null)
        {
            AttackingAudio.Play();
            Debug.Log("AttackingAudio played");
        }
        else
        {
            Debug.Log("AttackingAudio is null or has no clip");
        }
    }
    public void PlayJumpingAudio()
    {
        Debug.Log("PlayJumpingAudio called");
        if (JumpingAudio != null && JumpingAudio.clip != null)
        {
            Debug.Log("JumpingAudio - Volume: " + JumpingAudio.volume + " Mute: " + JumpingAudio.mute + " isPlaying: " + JumpingAudio.isPlaying);
            JumpingAudio.Play();
            Debug.Log("JumpingAudio play called");
        }
        else
        {
            Debug.Log("JumpingAudio is null or has no clip");
        }
    }
    public void PlayDeathAudio()
    {
        Debug.Log("PlayDeathAudio called");
        if (DeathAudio != null && DeathAudio.clip != null)
        {
            DeathAudio.Play();
            Debug.Log("DeathAudio played");
        }
        else
        {
            Debug.Log("DeathAudio is null or has no clip");
        }
    }

    public void PlaySwapAudio()
    {
        Debug.Log("PlaySwapAudio called");
        if (SwapAudio != null && SwapAudio.clip != null)
        {
            SwapAudio.Play();
            Debug.Log("SwapAudio played");
        }
        else
        {
            Debug.Log("SwapAudio is null or has no clip");
        }
    }

    public void PlayPickupAudio()
    {
        Debug.Log("PlayPickupAudio called");
        if (PickupAudio != null && PickupAudio.clip != null)
        {
            PickupAudio.Play();
            Debug.Log("PickupAudio played");
        }
        else
        {
            Debug.Log("PickupAudio is null or has no clip");
        }
    }

    public void PlayerWalkingAudio(bool Walking)
    {
        Debug.Log("PlayerWalkingAudio called with " + Walking);
        if (WalkingAudio == null || WalkingAudio.clip == null)
        {
            Debug.Log("WalkingAudio is null or has no clip");
            return;
        }

        Debug.Log("WalkingAudio - Volume: " + WalkingAudio.volume + " Mute: " + WalkingAudio.mute + " isPlaying: " + WalkingAudio.isPlaying);
        if (Walking && !WalkingAudio.isPlaying)
        {
            WalkingAudio.Play();
            Debug.Log("WalkingAudio play called");
        }
        else if (!Walking)
        {
            WalkingAudio.Stop();
            Debug.Log("WalkingAudio stopped");
        }
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
