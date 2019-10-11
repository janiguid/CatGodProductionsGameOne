using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBehavior : MonoBehaviour
{
    private RunnerManagerBehavior manager = null;
    private RunnerInputReceiver input = null;
    private int runnerID = -1;
    private int destinationTrack = -1;
    private bool alive = true;
    private float height = 1.0f;
    private bool dashing = false;
    private bool jumping = false;
    public float disadvantage = 0.0f;
    private static readonly float stdJumpDuration = 10.0f;
    private static readonly float stdDashDuration = 20.0f;
    private float remainingJumpDuration = 0.0f;
    private float remainingDashDuration = 0.0f;
    private static readonly float forwardRange = 2.0f;
    private static readonly float backwardRange = 1.0f;
    private static readonly float percentAttractiveForce = 10.0f;
    private float horizontalEffort = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D coll = gameObject.GetComponent<Collider2D>();
        if (coll != null)
        {
            height = coll.bounds.size.y;
        }
        input = gameObject.GetComponent<RunnerInputReceiver>();
    }

    /*  --GrantRunnerID--
        Called by RunnerManagerBehavior to form a parent connection
        back to the runner manager. Don't manually call this later
        on a different RunnerManagerBehavior, or else everything
        will go belly up. */
    public void GrantRunnerID(int id, RunnerManagerBehavior man)
    {
        manager = man;
        runnerID = id;
        destinationTrack = id;
    }

    /*  --HaveRunnerID--
        Indicates whether this instance has been assigned a runner
        ID by a runner manager. If not, we can't really do anything
        useful as a runner. */
    public bool HaveRunnerID()
    {
        return manager != null && runnerID >= 0;
    }

    /*  --RunnerID--
        Runner ID getter. */
    public int RunnerID()
    {
        return runnerID;
    }

    /*  --Alive--
        A runner is considered alive if it exists, has been assigned
        a runner ID, and either hasn't died, or hasn't died since
        it was last resurrected. If you're not alive, you're not
        in the game for now. */
    public bool Alive()
    {
        return gameObject.activeSelf && HaveRunnerID() && alive;
    }

    /*  --Kill, Resurrect--
        Alive setters. */
    public void Kill()
    {
        alive = false;
    }
    public void Resurrect()
    {
        alive = true;
    }

    /*  --Track--
        Returns the track the runner is on. This isn't just a getter
        for 'destinationTrack'; for game logic purposes, a runner
        is considered to be "on" a track if they're physically
        within its region. */
    public int Track()
    { 
        float raw =
            (manager.PlayAreaHeight()/2
                - gameObject.transform.position.y)
            / manager.TrackHeight() - 0.5f;
        int rounded = (int) Math.Round(raw);
        return manager.ConstrainID(rounded);
    }

    /*  --DestTrack--
        Getter for destinationTrack. */
    public int DestTrack()
    {
        return destinationTrack;
    }

    /*  --ChangingTracks--
        Returns whether the runner is changing tracks. This is
        determined by checking if the track the runner is on
        equals the track the runner wants to be on. */
    public bool ChangingTracks()
    {
        return Track() != destinationTrack;
    }

    /*  --OnTrack--
        Returns whether the runner is on the given track. A runner
        can technically be on more than one track at once, namely
        while transitioning between them, in which case OnTrack
        will return true for all tracks the runner is on, but Track
        will only return the track the runner is "most" on.
        Note, however, that unless the runner is changing tracks,
        the runner can only be considered to be on the track
        they've already changed to. So this rule about being
        on multiple tracks only applies when they are in fact
        changing tracks. */
    public bool OnTrack(int track)
    {
        if (!ChangingTracks())
        {
            return track == destinationTrack;
        }
        else
        {
            float top = gameObject.transform.position.y
                + height/2;
            float bottom = gameObject.transform.position.y
                - height/2;
            float trtop = manager.TrackTop(track);
            float trbottom = manager.TrackBottom(track);
            return (top > trtop && bottom < trtop)
                || (top > trbottom && bottom < trbottom)
                || (top < trtop && bottom > trbottom);
        }
    }

    /*  --MoveUpTrack, MoveDownTrack--
        Switches tracks. Using these functions, you can only switch
        to a track adjacent to the one you currently occupy
        or cancel such a switch already in progress. */
    public void MoveUpTrack()
    {
        int track = Track();
        if (track < destinationTrack)
        {
            destinationTrack = track;
        }
        else if (track == destinationTrack)
        {
            destinationTrack =
                manager.ConstrainID(destinationTrack - 1);
        }
    }
    public void MoveDownTrack()
    {
        int track = Track();
        if (track > destinationTrack)
        {
            destinationTrack = track;
        }
        else if (track == destinationTrack)
        {
            destinationTrack =
                manager.ConstrainID(destinationTrack + 1);
        }
    }

    /*  --ForceMoveTrack--
        Switches tracks while ignoring the rules outlined above. */
    public void ForceMoveTrack(int track)
    {
        destinationTrack = track;
    }

    /*  --Dashing--
        Returns whether the runner is dashing. Dashing is a possible
        player move that allows players to temporarily move farther
        ahead on the track. */
    public bool Dashing()
    {
        return dashing;
    }

    /*  --Jumping--
        Returns whether the runner is jumping.
        Jumping is a player's primary means of avoiding
        obstacles on the track. */
    public bool Jumping()
    {
        return jumping;
    }

    /*  --BeginDash, EndDash--
        Dash setters. */
    public void BeginDash()
    {
        dashing = true;
        remainingDashDuration = stdDashDuration;
    }
    public void EndDash()
    {
        dashing = false;
        remainingDashDuration = 0;
    }

    /*  --BeginJump, EndJump--
        Jump setters. */
    public void BeginJump()
    {
        jumping = true;
        remainingJumpDuration = stdJumpDuration;
    }
    public void EndJump()
    {
        jumping = false;
        remainingJumpDuration = 0;
    }

    /*  --ExtendJump--
        Adds to jump duration. */
    public void ExtendJump(float d)
    {
        jumping = true;
        remainingJumpDuration += d;
    }

    /*  --ExtendDash--
        Adds to jump duration. */
    public void ExtendDash(float d)
    {
        dashing = true;
        remainingDashDuration += d;
    }

    /*  --Manager--
        Manager getter. */
    public RunnerManagerBehavior Manager()
    {
        return manager;
    }

    /*  --AttractionPointX, AttractionPointY--
        The runner will be drawn toward this point. */
    public float AttractionPointX()
    {
        float x = manager.NeutralX(runnerID);
        return x + horizontalEffort - disadvantage;
    }
    public float AttractionPointY()
    {
        return manager.NeutralY(runnerID);
    }

    /*  --Knockback--
        Applies a disadvantage. This moves the attraction point
        backward from the neutral position given
        by RunnerManagerBehavior. Additionally, applies
        a disadvantageous impulse. */
    public void Knockback(float amount)
    {
        disadvantage += amount;
        //body.AddForce(amount*Vector2.left, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        Vector2 vecdiff =
            new Vector2(AttractionPointX(), AttractionPointY())
          - new Vector2(gameObject.transform.position.x,
                        gameObject.transform.position.y);
        //body.AddForce(vecdiff*percentAttractiveForce/100,
          //  ForceMode2D.Impulse);
        Vector2 v = vecdiff*percentAttractiveForce/100;
        gameObject.transform.position =
            new Vector3(
                gameObject.transform.position.x + v.x,
                gameObject.transform.position.y + v.y,
                gameObject.transform.position.z);
    }

    void Update()
    {
        remainingJumpDuration -= Time.deltaTime;
        if (remainingJumpDuration <= 0)
        {
            remainingJumpDuration = 0;
            jumping = false;
        }
        remainingDashDuration -= Time.deltaTime;
        if (remainingDashDuration <= 0)
        {
            remainingDashDuration = 0;
            dashing = false;
        }
        disadvantage -= Time.deltaTime*4;
        if (disadvantage <= 0)
        {
            disadvantage = 0;
        }
        ProcessInput();
        Console.WriteLine("{0} {1}", AttractionPointX(),
            AttractionPointY());
    }

    private void ProcessInput()
    {
        if (disadvantage > 0)
        {
            return;
        }
        float dx = input.X();
        if (dx > 0)
        {
            horizontalEffort = dx*forwardRange;
        }
        else
        {
            horizontalEffort = dx*backwardRange;
        }
        int oldDest = destinationTrack;
        float dy = input.Y();
        if (dy > 0.1)
        {
            MoveUpTrack();
        }
        else if (dy < -0.1)
        {
            MoveDownTrack();
        }
        if (!manager.runners[destinationTrack]
            .GetComponent<RunnerBehavior>().Alive())
        {
            destinationTrack = oldDest;
        }
    }
}
