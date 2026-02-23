using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapEffect : MonoBehaviour
{
    public int destroyTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyGameObject(destroyTimer));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyGameObject(int delayTime){

        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
