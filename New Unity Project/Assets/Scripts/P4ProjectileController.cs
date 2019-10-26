using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P4ProjectileController : MonoBehaviour
{
    public float speed = 10.0f;
    public float damage = 1.5f;
    public Vector3 moveTo; 
    public Animator animator;
    private float initF = 10f;
    private float maxScalingSpeed;
    private float currScale;
    private float initScale;
    private Vector3 newScale = new Vector3(1.5f, 1.5f, 1f);
    private int frames = 10;
    private bool collide = false;
    private bool collide2 = false;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 look = new Vector3(moveTo, transform.position.y, 0);
        //transform.LookAt(look);
        maxScalingSpeed = ((transform.localScale.x - newScale.x) * speed) /( 2.4f *  (Mathf.Abs(transform.position.x - moveTo.x)));
        maxScalingSpeed *= maxScalingSpeed;
        currScale = 0.01f;
        initScale = transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collide2) {
            if (frames < 0) {
                Destroy(gameObject);
            }
            else {
                frames--;
            }
        }
        else {
            if (moveTo.x > transform.position.x) {
                // transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * speed);
                currScale += 0.012f * maxScalingSpeed;
                transform.localScale = Vector3.Lerp(transform.localScale, newScale, Mathf.Sqrt(currScale) * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * 
                    // (speed * (initScale - transform.localScale.x) / transform.localScale.x));
                    speed);
                
            }
            else {
                collide = true;
                collide2 = true;
                animator.SetBool("collidable", true);
            }
        }
        
    }
    void OnTriggerStay2D(Collider2D pc) {
        if (collide && pc.CompareTag("Player")) {
            pc.GetComponent<RunnerBehavior>().AerialDamage(damage);
            collide = false;
        }
    }
    
}
