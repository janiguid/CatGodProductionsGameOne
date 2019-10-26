using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthup : MonoBehaviour
{
    //player needs a runnerbehavior script
    public float heals = 1;
    private RunnerBehavior rb = null;
    private float killTimer = 0.25f;
    private bool effectIncomplete = true;
    // Start is called before the first frame update
    void Start()
    {
        //player object needs to be set when this object is instantiated
        //position this above the player when they use it then it disappears after 0.25 seconds
        transform.Translate(0, 2, 0);    
    }

    // Update is called once per frame
    void Update()
    {
        if (effectIncomplete && rb != null ) 
        {
            rb.RecoverHealth(heals);
            effectIncomplete = false;
        }
        if (killTimer > 0) 
        {
            killTimer -= Time.deltaTime;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    //this obviously needs to be set
    public void SetPlayer(GameObject player) 
    {
        rb = player.GetComponent<RunnerBehavior>();
    }

}
