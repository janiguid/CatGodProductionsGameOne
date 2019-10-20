using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderObstacle : MonoBehaviour {

    //this needs to be in a separate script cuz of the pickup scripts
    void OnTriggerEnter2D(Collider2D pc)
    {
        //for the types that slow all players, will need to access all the players somehow
        if (pc.CompareTag("Player"))
        {
            RunnerBehavior rb = pc.GetComponent<RunnerBehavior>();
            SpawnablesController sc = gameObject.GetComponent<SpawnablesController>();
            // if the obstacle knocks back only one player
            // need the name of the script for this to work right
            if (sc.breakable && rb.Dashing())
            {
                Destroy(gameObject);
            }
            else if (sc.jumpable)
            {
                rb.Knockback(sc.knockbackDistance);
                rb.Damage(sc.damage);
                //need to find object with obstacle controller
                ObstacleController spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ObstacleController>();
                spawner.modSpeed(spawner.baseSpeed);
            }
            else
            {
                rb.AerialKnockback(sc.knockbackDistance);
                rb.AerialDamage(sc.damage);
                ObstacleController spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ObstacleController>();
                spawner.modSpeed(spawner.baseSpeed);
            }
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
