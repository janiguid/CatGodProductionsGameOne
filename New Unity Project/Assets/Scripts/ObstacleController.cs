using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{  
    public class Spawns {
        //0-whatever range => correlates to the index of object in the generatableObjects array
        public int generated {get; set;}
        //0-4 range => correlates to above vector2s of spawnpoints
        //current order is top middle bottom then top and bottom for the 2 lane objects 
        public int spawnpoint {get; set;}
        public int intervalToNext {get; set;}
        //object id then spawnpoint for constructor
        public Spawns(int gen, int sp, int intrvl) {generated = gen; spawnpoint = sp; intervalToNext = intrvl;}
        public void increaseInt(int x) {intervalToNext+= x;}
    }
    public int trackSize = 300;

    public float spawnInterval = 0.5f;
    public float currentSpeed = 3;
    //NOTE: each object needs a weight, these arrays need to be almost equal in length
    //the last weight is the chance for nothing to spawn
    //specify tracksize only
    public GameObject[] generatableObjects;
    public GameObject finishLine;
    public float[] generatableObjectWeights;
    public Vector2[] spawnpoints;

    private static int TRACK_DEFAULT_CAPACITY = 300;
    private List<Spawns> track = new List<Spawns>(TRACK_DEFAULT_CAPACITY);
    private float timer;
    private int trackIndex = 0;
    // private List<RunnerManagerBehavior> runman;
    private RunnerManagerBehavior runman;

    //start inclusive, end not inclusive
    // private int rouletteWheelSpawn(int start, int total) {
    //     int ret = -1;
    //     float r = Random.value * total;
    //     for (int i = 1; i <= total; i++) {
    //         if (r <= i) {
    //             ret = i-1;
    //             break;
    //         }
    //     }
    //     return ret + start;
    // }
    private List<Vector2> PrepareViableSpawnpoints() {
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
    public void modSpeed(float speed) {
        var foundObjects = FindObjectsOfType<SpawnablesController>();
        foreach (SpawnablesController spawnables in foundObjects ) {
            spawnables.speed = speed;
        }
    }
    //currently have it so tracks are 1, 2, 3 from top to bottom, change at will :p
    //run everytime a track is destroyed
    public void trackDestroyed(int trackDesignation) {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //need to create the track - will just be a 3d struct with [objectType, intervalToNext, position]
        //this will need to be proc gen handled and difficulty should increase over duration of level
        //also need to be able to increase/decrease speed of stuff
        //so need a difficulty 


        //need to normalize the generated object weights to be from 0 - 1
        //so first add all the weights then multiply each weight by its proportion of total weights
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
        // runman = GameObject.FindGameObjectsWithTag("Player");
        runman = FindObjectOfType<RunnerManagerBehavior>();
        List<int> genSpawnpointCounts = new List<int>(15);
        for (int i = 0; i < generatableObjects.Length; i++) {
            GameObject toKill = Instantiate(generatableObjects[i], spawnpoints[0], Quaternion.identity);
            genSpawnpointCounts.Add(toKill.GetComponent<SpawnablesController>().spawnpointCount);
            Destroy(toKill);
        }
        for (int i = 0; i < trackSize; i++) {
            float r = Random.value;
            int objIndex = -1;
            int spIndex = -1;
            for (int j = 0; j < generatableObjects.Length; j++) {
                if (r <= generatableObjectWeights[j]) {
                    objIndex = j;
                    if (genSpawnpointCounts[j] == 3) {
                        // spIndex = rouletteWheelSpawn(0, 3);
                        spIndex = Random.Range(0, 3);
                    }
                    else if (genSpawnpointCounts[j] == 2) {
                        // spIndex = rouletteWheelSpawn(3, 2);
                        spIndex = Random.Range(3, 5);
                    }
                    else {
                        spIndex = 1;
                    }
                    //base interval to next spawn is always one
                    track.Add(new Spawns(objIndex, spIndex, 1));
                    break;
                   
                }
            }
            if (objIndex == -1 && track.Count > 0) {
                track[track.Count - 1].increaseInt(1);
            }
        }
        //increase interval to the last spawn object
        track[track.Count - 1].increaseInt(3);
    }



    // Update is called once per frame
    void Update()
    {
        //now we get to switch this to reading out from the spawn list
        //and just spawning stuff
        //when the index of the track is at the count 
        //spawn the finish line
        // if (timer < 0) {
        //     //the temporary (?) great roulette wheel of justice
        //     float r = Random.value;
        //     for (int j = 0; j < generatableObjects.Length; j++) {
        //         if (r <= generatableObjectWeights[j]) {
        //             Vector2 spawn = spawnpoints[0];
        //             List<Vector2> viableSpawnpoints = PrepareViableSpawnpoints();
        //             r = Random.value * viableSpawnpoints.Count;
        //             //randomize the spawn point too, this one is currently unweighted
        //             for (int i = 1; i <= viableSpawnpoints.Count; i++) {
        //                 if (r <= i) {
        //                     spawn = viableSpawnpoints[i-1];
        //                     break;
        //                 }
        //             }
        //             GameObject obs = Instantiate(generatableObjects[j], spawn, Quaternion.identity);
        //             obs.GetComponent<SpawnablesController>().speed = currentSpeed;
        //             break;
        //         }
        //     }
        //     timer = spawnInterval;
        // }
        // else {
        //     timer -= Time.deltaTime;
        // }
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else {
            if (trackIndex < track.Count) {
                //spawn from track
                Spawns temp = track[trackIndex++];
                GameObject obs = Instantiate(generatableObjects[temp.generated], spawnpoints[temp.spawnpoint], Quaternion.identity);
                obs.GetComponent<SpawnablesController>().speed = currentSpeed;
                timer = spawnInterval * temp.intervalToNext;
            }
            else {
                //spawn finish line
                GameObject obs = Instantiate(finishLine, spawnpoints[1], Quaternion.identity);
                obs.GetComponent<SpawnablesController>().speed = currentSpeed;
                
                //to test the new function to mod the speed of all objects on screen
                //modSpeed(11);
                enabled = false;
            }
        }
    }
}
