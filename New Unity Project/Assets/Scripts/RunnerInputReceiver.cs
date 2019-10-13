using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInputReceiver : MonoBehaviour
{
    private float x = 0, y = 0;
    private bool jump = false, dash = false;
    private RunnerBehavior rb;

    public void SetAxes(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public void SetJump(bool b)
    {
        jump = b;
    }
    public void SetDash(bool b)
    {
        dash = b;
    }

    public float X()
    {
        return x;
    }
    public float Y()
    {
        return y;
    }
    public bool Jump()
    {
        return jump;
    }
    public bool Dash()
    {
        return dash;
    }

    public void Start()
    {
        rb = gameObject.GetComponent<RunnerBehavior>();
    }
}
