using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float spawnInterval = 0.5f;
    public float currentSpeed = 3;
    //NOTE: each object needs a weight, these arrays need to be almost equal in length
    //the last weight is the chance for nothing to spawn
    public GameObject[] generatableObjects;
    public float[] generatableObjectWeights;
    public Vector2[] spawnpoints;
    
    private float timer;

    private RunnerManagerBehavior runman;

    // Start is called before the first frame update
    void Start()
    {
        //need to create the track - will just be a 3d struct with [objectType, intervalToNext, position]
        //this will need to be proc gen handled and difficulty should increase over duration of level
        //also need to be able to increase/decrease speed of stuff
        //so need a difficulty 


        //need to normalize the generated object weights to be from 0 - 1
        //so first add all the weights then multiply each weight by its proportion of total weights then multiply that by 1000
        float totalW = 0;
        float wSoFar = 0;
        foreach (float w in generatableObjectWeights) {
            totalW += w;
        }
        for (int i = 0; i < generatableObjectWeights.Length; i++) {
            wSoFar += generatableObjectWeights[i];
            generatableObjectWeights[i] = wSoFar / totalW;
        }
        timer = spawnInterval;
        runman = FindObjectOfType<RunnerManagerBehavior>();
    }

    private List<Vector2> PrepareViableSpawnpoints()
    {
        List<Vector2> ret = new List<Vector2>();
        foreach (Vector2 v2 in spawnpoints)
        {
            if (runman.Alive(runman.TrackAt(v2.y)))
            {
                ret.Add(v2);
            }
        }
        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0) {
            //the temporary (?) great roulette wheel of justice
            float r = Random.value;
            for (int j = 0; j < generatableObjects.Length; j++) {
                if (r <= generatableObjectWeights[j]) {
                    Vector2 spawn = spawnpoints[0];
                    List<Vector2> viableSpawnpoints = PrepareViableSpawnpoints();
                    r = Random.value * viableSpawnpoints.Count;
                    //randomize the spawn point too, this one is currently unweighted
                    for (int i = 1; i <= viableSpawnpoints.Count; i++) {
                        if (r <= i) {
                            spawn = viableSpawnpoints[i-1];
                            break;
                        }
                    }
                    GameObject obs = Instantiate(generatableObjects[j], spawn, Quaternion.identity);
                    obs.GetComponent<SpawnablesController>().speed = currentSpeed;
                    break;
                }
            }
            timer = spawnInterval;
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
