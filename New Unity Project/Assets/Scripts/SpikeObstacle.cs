using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeObstacle : MonoBehaviour {
    public float knockbackDistance = 1f;

    void OnTriggerEnter2D(Collider2D pc) {
    //for the types that slow all players, will need to access all the players somehow
        if (pc.CompareTag("Player")) {
            // if the obstacle knocks back only one player
            // need the name of the script for this to work right
            pc.GetComponent<RunnerBehavior>().Knockback(knockbackDistance);
            Debug.Log("Knocked back " + knockbackDistance);
            // if the obstacle knocks back or did something to all the players --> grab all the tagged players 
            // GameObject[] players;
            // players = GameObject.FindGameObjectsWithTag("Player");
            // foreach (GameObject player in players) {
            //     player.GetComponent<RunnerBehavior>().Knockback(knockbackDistance);
            // }
        }
    }
}
