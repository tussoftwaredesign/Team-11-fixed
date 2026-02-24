using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Open")]
    public void Open() {
        animator.SetTrigger("Open");
    }
    
}
