using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    public float weaponTimer = 3.0f;
    public float impactDelay = 0.25f;
    public float projectileSpeed = 3.0f;
    public float projectileDamage = 1.0f;
    public bool skipNextShot;
    public Vector2 cursorStart;
    private Rigidbody2D rbody;
    private float timer = 0;
    public GameObject CrosshairStamp;
    private static readonly string[] controls = {"P4 Horizontal", "P4 Vertical", "P4 Jump", "P4 Dash"};

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        skipNextShot = true;
    }
    private void Movement() {
        float x = Input.GetAxis(controls[0]);
        float y = Input.GetAxis(controls[1]);
        Vector3 dir = new Vector3(Input.GetAxis(controls[0]), Input.GetAxis(controls[1]), 0).normalized;
        rbody.MovePosition(transform.position + dir * Time.deltaTime * 10);
        // transform.position += dir * Time.deltaTime * 3;
        //transforms from world to viewport back to world after clamping in viewport mode
        //credit : https://answers.unity.com/questions/799656/how-to-keep-an-object-within-the-camera-view.html
        dir = Camera.main.WorldToViewportPoint (transform.position);
        dir.x = Mathf.Clamp01(dir.x);
        dir.y = Mathf.Clamp01(dir.y);
        transform.position = Camera.main.ViewportToWorldPoint(dir);
    }
    void Update()
    {
        Movement();
        if (timer > 0) {
            //cant check for shooting
            timer -= Time.deltaTime;
        }
            //can start checking for shooting
            //i guess i'll use the jump axis
        else if (skipNextShot) {
            timer = weaponTimer; 
            skipNextShot = false;    
        }
        else {
            float pressed = Input.GetAxis(controls[2]);
            if (pressed > 0.1f) {
                Debug.Log(pressed);
                //spawn a crosshairs stamp in this location
                GameObject stamp = Instantiate(CrosshairStamp, transform.position, Quaternion.identity);
                //after delay a projectile will spawn from off screen (the furthest corner) and impact here
                StampController sc = stamp.GetComponent<StampController>();
                sc.delay = impactDelay;
                sc.speed = projectileSpeed;
                sc.damage = projectileDamage;
                timer = weaponTimer;
            }    
        }
    }
}

