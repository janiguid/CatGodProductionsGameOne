using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public float speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D obs) 
    {
        if (obs.CompareTag("Obstacle")) 
        {
            obs.GetComponent<BoulderObstacle>().Break();
        }
    }

    void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
