using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnablesController : MonoBehaviour {
    
    public float speed = 1;
    public int spawnpointCount = 3;
    public bool jumpable = true;
    public bool destroying = false;
    public float knockbackDistance = 1f;
    public float damage = 0f;
    public bool breakable = true;
    private float timeToDestroy = 0.35f;
    public int lane;
    
    //speed will be modified by the obstacle controller too. 
    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (destroying)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    //this can just be done as soon as they're offscreen which is when onbecameinvisible gets called
    //unless theres some reason we want this delayed
    void OnBecameInvisible() {
        // Destroy(gameObject);
        destroying = true;
        if (gameObject.GetComponent<FinishLine>() != null)
        {
            SceneManager.LoadScene("End Scene");

        }
    }
   
    
}
