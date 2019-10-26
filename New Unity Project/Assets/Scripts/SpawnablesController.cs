using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnablesController : MonoBehaviour {
    
    public float speed = 1;
    public int spawnpointCount = 3;
    public bool jumpable = true;
    public bool destroying = false;
    public float knockbackDistance = 1f;
    public float damage = 0f;
    public bool breakable = true;
    private float timeToDestroy = 0.35f;
    public int lane;
    
    //speed will be modified by the obstacle controller too. 
    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (destroying)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    //this can just be done as soon as they're offscreen which is when onbecameinvisible gets called
    //unless theres some reason we want this delayed
    void OnBecameInvisible() {
        // Destroy(gameObject);
        destroying = true;
    }
    //void killObstacle() {
        //wanna have like a particle emitter effect maybe?
      //  Destroy(gameObject);
    //}

    //moved to a different script
    /*void OnTriggerEnter2D(Collider2D pc) {
    //for the types that slow all players, will need to access all the players somehow
        if (pc.CompareTag("Player")) {
            RunnerBehavior rb = pc.GetComponent<RunnerBehavior>();
            // if the obstacle knocks back only one player
            // need the name of the script for this to work right
            if (breakable && rb.Dashing())
            {
                Destroy(gameObject);
            }
            else if (jumpable)
            {
                rb.Knockback(knockbackDistance);
                rb.Damage(damage);
            }
            else
            {
                rb.AerialKnockback(knockbackDistance);
                rb.AerialDamage(damage);
            }
            // Debug.Log("Knocked back " + knockbackDistance);
            // if the obstacle knocks back or did something to all the players --> grab all the tagged players 
            // GameObject[] players;
            // players = GameObject.FindGameObjectsWithTag("Player");
            // foreach (GameObject player in players) {
            //     player.GetComponent<RunnerBehavior>().Knockback(knockbackDistance);
            // }
        }
    }*/
    public void Break() {
        if (breakable) {
            Destroy(gameObject);
        }
    }
}
