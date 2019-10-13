using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnablesController : MonoBehaviour {
    public float speed = 1;

    //speed will be modified by the obstacle controller too. 
    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
