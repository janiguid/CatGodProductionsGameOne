using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    // private float deathTimer = 0.25f;
    public float speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    if (deathTimer > 0) 
    //    {
    //        deathTimer -= Time.deltaTime;
    //    } 
    //    else 
    //    {
    //        Destroy(gameObject);
    //    }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D obs) 
    {
        if (obs.CompareTag("Obstacle")) 
        {
            obs.GetComponent<BoulderObstacle>().Break();
        }
    }

    void OnBecameInvisible() {
        //Destroy(gameObject);
        Destroy(gameObject);
    }
}
