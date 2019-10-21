using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P4ProjectileController : MonoBehaviour
{
    public float speed = 2.0f;
    public float damage = 1.0f;
    public float moveTo; 
    
    private int frames = 5;
    private bool collide = false;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 look = new Vector3(moveTo, transform.position.y, transform.position.z);
        transform.LookAt(look);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (collide) {
            if (frames < 0) {
                Destroy(gameObject);
            }
            else {
                frames--;
            }
        }
        else {
            if (moveTo > transform.position.x) {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, 
                    transform.position.y, transform.position.z);
            }
            else {
                collide = true;
            }
        }
        
    }
    void OnTriggerStay2D(Collider2D pc) {
        if (collide && pc.CompareTag("Player")) {
            pc.GetComponent<RunnerBehavior>().AerialDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
