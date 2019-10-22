﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public float delay = 0.25f;
    public float selfDestructDelay = 0.75f;
    public float speed = 3.0f;
    public float damage = 1.0f;
    public Vector3 spawnpoint = new Vector3(-10, 0, 0);
    private float timer;
    private float lifespan;
    private bool spawned = false;
    public GameObject Projectile;
    // Start is called before the first frame update
    void Start()
    {
        timer = delay; 
        lifespan = selfDestructDelay;  
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            lifespan -= Time.deltaTime;
        }
        else if (lifespan < 0) {
            Destroy(gameObject);
        }
        else {
            //send a projectile to this object
            //gotta check transform position relative to viewport outer settings
            // Vector3 spawnpoint = Camera.main.WorldToViewportPoint(transform.position);
            // //this weird looking math shit rounds the number then gives 0 if 1 and 1 if 0
            // //idk if this is actually faster than an if statement...
            // spawnpoint.x = (Mathf.Round(spawnpoint.x) - 1 ) * (-1);
            // spawnpoint.y = (Mathf.Round(spawnpoint.y) - 1 ) * (-1);
            spawnpoint.y = transform.position.y;
            GameObject proj = Instantiate(Projectile, spawnpoint, Quaternion.identity);
            //gotta send this object the stamps position
            P4ProjectileController pc = proj.GetComponent<P4ProjectileController>();
            pc.speed = speed;
            pc.damage = damage;
            pc.moveTo = transform.position.x;
            timer = selfDestructDelay;
        }
        
    }
}