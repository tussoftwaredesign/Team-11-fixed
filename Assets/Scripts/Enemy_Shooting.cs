using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject big;
    private GameObject mid;
    private GameObject small;

    // Start is called before the first frame update
    void Start()
    {
        big = GameObject.FindGameObjectWithTag("Big Guy");
        mid = GameObject.FindGameObjectWithTag("Mid Guy");
        small = GameObject.FindGameObjectWithTag("Small Guy");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3)
        {
            timer = 0;
            shoot();
        }
  
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
