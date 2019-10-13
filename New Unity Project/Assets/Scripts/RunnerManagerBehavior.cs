using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManagerBehavior : MonoBehaviour
{
    /*  --runners, runnerBehaviors--
        Note that runners' indices in these arrays
        are defined as their "runner ID." Most functions
        in the rest of this class provide data about runners
        by their runner ID, not by reference. */
    public GameObject[] runners;
    private RunnerBehavior[] runnerBehaviors;
    private float neutralPosition;
    private float dashPosition;
    public float playAreaYOffset = 0.0f;
    private static readonly bool drawTrackBoundsForDebug = true;
    private InventoryManager inventoryManager = null;
    private RunnerInventory[] runnerInventories;

    /*  --PlayAreaHeight, PlayAreaWidth--
        Gets the dimensions of the game world.
        Thanks to dakotapearl on answers.unity.com
        for the implementations. */
    public float PlayAreaHeight()
    {
        return Camera.main.orthographicSize*2.0f - playAreaYOffset;
    }
    public float PlayAreaWidth()
    {
        return Camera.main.orthographicSize*2.0f*Camera.main.aspect;
    }

    /*  --ConstrainID--
        Clamps the given integer to the range of valid
        runner and track IDs. */
    public int ConstrainID(int id)
    {
        if (id < 0)
        {
            return 0;
        }
        if (id >= runners.Length)
        {
            return runners.Length - 1;
        }
        return id;
    }

    // Start is called before the first frame update
    void Start()
    {
        neutralPosition = PlayAreaWidth()/6 - PlayAreaWidth()/2;
        dashPosition = 3*PlayAreaWidth()/8 - PlayAreaWidth()/2;
        runnerBehaviors = new RunnerBehavior[runners.Length];
        for (int i = 0; i < runners.Length; ++i)
        {
            if (runners[i] != null)
            {
                runnerBehaviors[i] =
                    runners[i].GetComponent<RunnerBehavior>();
                if (runnerBehaviors[i] == null)
                {
                    Console.Error.WriteLine(
                        "WARN: Cannot register non-runner " +
                        "GameObject with runner manager " +
                        "at player index {0}.",
                    i);
                    runners[i] = null;
                }
                else
                {
                    runnerBehaviors[i].GrantRunnerID(i, this);
                }
            }
        }
        inventoryManager =
            FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            runnerInventories = new RunnerInventory[
                inventoryManager.Inventories.Length];
            if (runnerInventories.Length != runners.Length)
            {
                Console.Error.WriteLine(
                    "WARN: Inventory count {0} does not match " +
                    "runner count {1}.",
                    runners.Length,
                    runnerInventories.Length
                );
            }
            for (int i = 0;
                i < runnerInventories.Length;
                ++i)
            {
                runnerInventories[i]
                    = inventoryManager.Inventories[i];
            }
        }
    }

    /*  --Alive--
        Returns the list of runners still alive. */
    public List<int> Alive()
    {
        List<int> ret = new List<int>();
        foreach (RunnerBehavior rb in runnerBehaviors)
        {
            if (rb != null && rb.Alive())
            {
                ret.Add(rb.RunnerID());
            }
        }
        return ret;
    }

    /*  --OnTrack--
        Returns the list of runners on any given track.
        There are as many tracks as there are runners.
        Each runner starts on the track whose ordinal from the top
        matches their runner ID. When a runner with a given
        runner ID dies, the respective matching track
        is disabled. */
    public List<int> OnTrack(int track)
    {
        track = ConstrainID(track);
        List<int> ret = new List<int>();
        foreach (int i in Alive())
        {
            if (runnerBehaviors[i].OnTrack(track))
            {
                ret.Add(runnerBehaviors[i].RunnerID());
            }
        }
        return ret;
    }

    /*  --TrackHeight--
        Returns the height of the tracks. */
    public float TrackHeight()
    {
        return PlayAreaHeight()/runners.Length;
    }

    /*  --TrackTop, TrackCenter, TrackBottom--
        Returns the Y coordinates of the respective parts
        of the given track. */
    public float TrackTop(int track)
    {
        track = ConstrainID(track);
        return PlayAreaHeight()/2 - TrackHeight()*track + playAreaYOffset;
    }
    public float TrackCenter(int track)
    {
        track = ConstrainID(track);
        return TrackTop(track) - TrackHeight()/2;
    }
    public float TrackBottom(int track)
    {
        track = ConstrainID(track);
        return TrackTop(track) - TrackHeight();
    }

    /*  --NeutralX, NeutralY--
        Given a runner ID, returns the coordinates of the point
        the runner should generally occupy. Calculates these
        coordinates based on the runner's current track, and,
        if applicable, other runners on that track. */
    public float NeutralX(int who)
    {
        who = ConstrainID(who);
        RunnerBehavior rb = runnerBehaviors[who];
        if (rb == null || !rb.Alive())
        {
            return -PlayAreaWidth();
        }
        else if (rb.Dashing())
        {
            return dashPosition;
        }
        else
        {
            return neutralPosition;
        }
    }
    public float NeutralY(int who)
    {
        who = ConstrainID(who);
        RunnerBehavior rb = runnerBehaviors[who];
        if (rb == null || !rb.Alive())
        {
            GameObject go = runners[who];
            if (go == null)
            {
                return 0;
            }
            else
            {
                return go.transform.position.y;
            }
        }
        else
        {
            int track = ConstrainID(rb.DestTrack());
            List<int> neighbors = OnTrack(track);
            int ordinal = -1;
            for (int i = 0; i< neighbors.Count; ++i)
            {
                if (neighbors[i] == who)
                {
                    ordinal = i;
                    break;
                }
            }
            if (ordinal < 0)
            {
                return TrackCenter(track);
            }
            else
            {
                return TrackTop(track) -
                    (ordinal + 1)*TrackHeight()
                    / (neighbors.Count + 1);
            }
        }
    }

    private void DrawHorizontalLine(float y, float r, float g, float b)
    {
        Debug.DrawLine(
            new Vector3(-PlayAreaWidth()/2, y, 0),
            new Vector3(PlayAreaWidth()/2, y, 0),
            new Color(r, g, b)
        );
    }

    void FixedUpdate()
    {
        if (drawTrackBoundsForDebug)
        {
            for (int i = 0; i < runners.Length; ++i)
            {
                DrawHorizontalLine(TrackTop(i) - 0.05f,
                    1.0f, 0.0f, 0.0f);
                DrawHorizontalLine(TrackCenter(i),
                    0.0f, 1.0f, 0.0f);
                DrawHorizontalLine(TrackBottom(i) + 0.05f,
                    0.0f, 0.0f, 1.0f);
                if (!runnerBehaviors[i].Alive())
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        DrawHorizontalLine(
                            TrackTop(i) - TrackHeight()*(j + 1)/9,
                            1.0f, 1.0f, 1.0f);
                    }
                }
            }
        }
    }

    public RunnerInventory GetInventory(int runnerID)
    {
        runnerID = ConstrainID(runnerID);
        if (runnerID < runnerInventories.Length)
        {
            return runnerInventories[runnerID];
        }
        else
        {
            Console.Error.WriteLine(
                "WARN: Tried to access a missing inventory " +
                "for player {0}.",
            runnerID);
            return null;
        }
    }

    public GlobalItemData GetItemData()
    {
        return inventoryManager.itemData;
    }
}
