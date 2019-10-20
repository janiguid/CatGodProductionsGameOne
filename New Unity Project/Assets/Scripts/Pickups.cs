using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private static readonly string[] pickups = { "baoig", "gionb", "oibgoib", "goigan"};
    void OnTriggerEnter2D(Collider2D pc) {
    //dont have any item prefabs and not sure how to interface with global item data
        if (pc.CompareTag("Player")) {
            /*RunnerBehavior rb = pc.GetComponent<RunnerBehavior>();
            int r = Random.Range(0, pickups.Length);
            rb.Pickup(pickups[r]);*/
            Destroy(gameObject);
        }
    }
}
