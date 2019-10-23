using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleController : MonoBehaviour
{  
    public class Spawns {
        //0-whatever range => correlates to the index of object in the generatableObjects array
        public int generated {get; set;}
        //0-4 range => correlates to above vector2s of spawnpoints
        //current order is top middle bottom then top and bottom for the 2 lane objects 
        public int spawnpoint {get; set;}
        public int index { get; set; }
        public int intervalToNext {get; set;}
        //object id then spawnpoint for constructor
        public Spawns(int gen, int sp, int intrvl, int ind) { generated = gen; spawnpoint = sp; intervalToNext = intrvl; index = ind; }
        public void increaseInt(int x) {intervalToNext+= x;}
    }
    public int trackSize = 300;
    private int pickupID = 2;
    public float spawnInterval = 0.5f;
    public float speedIncrement = 0.5f;
    public float baseSpeed = 3;
    private float currentSpeed;
    public float maxSpeed = 6;
    //NOTE: each object needs a weight, these arrays need to be almost equal in length
    //the last weight is the chance for nothing to spawn
    //specify tracksize only
    public GameObject[] generatableObjects;
    public GameObject finishLine;
    public float[] generatableObjectWeights;
    public Vector2[] spawnpoints;

    private bool[] living_lanes = { true, true, true, true, true };
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
    //sets speed to passed speed
    public void modSpeed(float speed) {
        var foundObjects = FindObjectsOfType<SpawnablesController>();
        foreach (SpawnablesController spawnables in foundObjects ) {
            spawnables.speed = speed;
        }
    }
    //currently have it so tracks are 0, 1, 2 from top to bottom, change at will :p
    //run everytime a track is destroyed
    //1 des -> destroy 0 and 
    //this thing breaks when 2 players die at the exact same time...
    //had to change it to dynamically checking the living_lanes bool list
    public void trackDestroyed(int trackDesignation) {

        /* //this part is for later when we have an actual track being destroyed
        var foundObjects = FindObjectsOfType<SpawnablesController>();
        foreach (SpawnablesController spawnables in foundObjects)
        {
            int lane = spawnables.lane;
            if (trackDesignation != 2)
            {
                if (lane == trackDesignation || lane == 3)
                {
                    spawnables.destroying = true;
                }
            }
            else
            {
                if (lane == trackDesignation || lane == 4)
                {
                    spawnables.destroying = true;
                }
            }
        }*/
        living_lanes[trackDesignation] = false;
        if (trackDesignation == 0)
        {
            //destroy td and 3 and only items past the current trackindex
           // track.RemoveAll(item => item.index > trackIndex && (item.spawnpoint == trackDesignation || item.spawnpoint == 3));
            
            living_lanes[3] = false;
        }
        else if (trackDesignation == 1)
        {
            //destroy td and 4 and only items past the current trackindex
            //track.RemoveAll(item => item.index > trackIndex && (item.spawnpoint == trackDesignation || item.spawnpoint == 4));
            living_lanes[3] = false;
            living_lanes[4] = false;
        }
        else
        {
            living_lanes[4] = false;
        }
        for (int i = 0; i < living_lanes.Length; i++)
        {
            if (living_lanes[i])
            {
                return;
            }
        }
        //go to the endgame screen
        SceneManager.LoadScene("End Scene");
    }
    // Start is called before the first frame update
    void Start()
    {
        //need to normalize the generated object weights to be from 0 - 1
        //so first add all the weights then multiply each weight by its proportion of total weights
        currentSpeed = baseSpeed;
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
        int indTracker = 0;
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
                        track.Add(new Spawns(objIndex, spIndex, 1, indTracker++));
                    }
                    else if (genSpawnpointCounts[j] == 2) {
                        // spIndex = rouletteWheelSpawn(3, 2);
                        spIndex = Random.Range(3, 5);
                        track.Add(new Spawns(objIndex, spIndex, 1, indTracker++));
                    }
                    //spawn pickups
                    else {
                        track.Add(new Spawns(objIndex, 0, 0, indTracker++));
                        track.Add(new Spawns(objIndex, 1, 0, indTracker++));
                        track.Add(new Spawns(objIndex, 2, 1, indTracker++));
                    }
                    //base interval to next spawn is always one
                    
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


    public float GetTime()
    {
        return currentSpeed * Time.deltaTime ;
    }

    //spawn from the spawns object
    private void SpawnObject(Spawns sp)
    {
        if (living_lanes[sp.spawnpoint])
        {
            GameObject obs = Instantiate(generatableObjects[sp.generated], spawnpoints[sp.spawnpoint], Quaternion.identity);
            obs.GetComponent<SpawnablesController>().speed = currentSpeed;
            obs.GetComponent<SpawnablesController>().lane = sp.spawnpoint;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0) {
            print(timer);
            timer -= Time.deltaTime;
        }
        else {
            if (trackIndex < track.Count) {
                //spawn from track
                Spawns temp = track[trackIndex++];
                if (temp.generated != pickupID)
                {
                    SpawnObject(temp);
                }
                else
                {
                //pickups get fancy logic to account for track destruction
                    do
                    {
                        SpawnObject(temp);
                        temp = track[trackIndex++];
                    } while (temp.generated == pickupID);
                    trackIndex--;
                    temp = track[trackIndex];
                }
                timer = spawnInterval * temp.intervalToNext;
                //every 10th item spawned increases the speed
                if (trackIndex % 10 == 0 && (currentSpeed + speedIncrement) < maxSpeed)
                {
                    currentSpeed += speedIncrement;
                    modSpeed(currentSpeed);
                }
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
