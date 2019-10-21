using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    public float weaponTimer = 3.0f;
    public float impactDelay = 0.25f;
    public Vector2 cursorStart;
    //private float dashPosition;
    public float playAreaYOffset = 0.0f;
    private static readonly bool drawTrackBoundsForDebug = true;


    //stole this, thanks jaime lol
    /*  --PlayAreaHeight, PlayAreaWidth--
        Gets the dimensions of the game world.
        Thanks to dakotapearl on answers.unity.com
        for the implementations. */
    public float PlayAreaHeight()
    {
        return Camera.main.orthographicSize * 2.0f - playAreaYOffset;
    }
    public float PlayAreaWidth()
    {
        return Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void DrawHorizontalLine(float y, float r, float g, float b)
    {
        Debug.DrawLine(
            new Vector3(-PlayAreaWidth() / 2, y, 0),
            new Vector3(PlayAreaWidth() / 2, y, 0),
            new Color(r, g, b)
        );
    }

    void Update()
    {

    }
}

