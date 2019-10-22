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
        //Vector3 look = new Vector3(moveTo, transform.position.y, 0);
        //transform.LookAt(look);
        transform.Rotate(0,0,90);
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
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
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
