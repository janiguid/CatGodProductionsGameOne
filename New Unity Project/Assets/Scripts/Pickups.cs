using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private GameObject[] pickups = null;
    //private static readonly string[] pickups = { "baoig", "gionb", "oibgoib", "goigan"};
    void OnTriggerEnter2D(Collider2D pc) {
    //dont have any item prefabs and not sure how to interface with global item data
        if (pc.CompareTag("Player") && pickups != null) {
            int r = Random.Range(0, pickups.Length);
            InventoryData id = pc.transform.GetChild(0).GetComponent<RunnerInventory>().MyData;
            id.AddItem(pickups[r]);  
            Destroy(gameObject);
        }
    }
    public void SetPickups(GameObject[] pu) {
        pickups = pu;
    }
}
