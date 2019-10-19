using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnablesController : MonoBehaviour {
    
    public float speed = 1;
    public int spawnpointCount = 3;
    public bool jumpable = true;
    public float knockbackDistance = 1f;
    //speed will be modified by the obstacle controller too. 
    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
    void killObstacle() {
        //wanna have like a particle emitter effect maybe?
    }
    void OnTriggerEnter2D(Collider2D pc) {
    //for the types that slow all players, will need to access all the players somehow
        if (pc.CompareTag("Player")) {
            // if the obstacle knocks back only one player
            // need the name of the script for this to work right
            pc.GetComponent<RunnerBehavior>().Knockback(knockbackDistance);
            // Debug.Log("Knocked back " + knockbackDistance);
            // if the obstacle knocks back or did something to all the players --> grab all the tagged players 
            // GameObject[] players;
            // players = GameObject.FindGameObjectsWithTag("Player");
            // foreach (GameObject player in players) {
            //     player.GetComponent<RunnerBehavior>().Knockback(knockbackDistance);
            // }
        }
    }
}
