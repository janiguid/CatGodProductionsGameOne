using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float spawnInterval = 0.5f;
    public float currentSpeed = 3;
    //NOTE: each object needs a weight, these need to be equal in length
    public GameObject[] generatableObjects;
    public int[] generatableObjectWeights;
    public Vector2 spawnpoint;
    public Vector2 spawnpoint1;
    public Vector2 spawnpoint2;
    
    private float timer;
   
    // Start is called before the first frame update
    void Start()
    {
        //need to create the track - will just be a 3d struct with [objectType, intervalToNext, position]
        //this will need to be proc gen handled and difficulty should increase over duration of level
        //also need to be able to increase/decrease speed of stuff
        //so need a difficulty 


        //need to normalize the generated object weights to be from 0 - 1000
        //so first add all the weights then multiply each weight by its proportion of total weights then multiply that by 1000

        //whatever last option is gets auto gen'd to account for possible bad maths if nothing else got spawned
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0) {
            //the temporary great roulette wheel of justice
            float r = Random.value;
            GameObject obs = Instantiate(generatableObjects[0], transform.position, Quaternion.identity);
            obs.GetComponent<SpawnablesController>().speed = currentSpeed;
            timer = spawnInterval;
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
