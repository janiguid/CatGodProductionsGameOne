using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInputDispatcher : MonoBehaviour
{
    private enum Controls {X, Y, Jump, Dash}
    public int keyboardIsFor = 0;
    private static readonly string[][] controls =
    {
        new string[]
        {
            "P1 Horizontal",
            "P1 Vertical",
            "P1 Jump",
            "P1 Dash"
        },
        new string[]
        {
            "P2 Horizontal",
            "P2 Vertical",
            "P2 Jump",
            "P2 Dash"
        },
        new string[]
        {
            "P3 Horizontal",
            "P3 Vertical",
            "P3 Jump",
            "P3 Dash"
        }/* ,
        new string[]
        {
            "P4 Horizontal",
            "P4 Vertical",
            "P4 Jump",
            "P4 Dash"
        }*/
    };

    private RunnerManagerBehavior rmb;
    private RunnerInputReceiver[] receivers;

    // Start is called before the first frame update
    void Start()
    {
        rmb = gameObject.GetComponent<RunnerManagerBehavior>();
        if (rmb == null)
        {
            Console.Error.WriteLine(
                "WARN: RunnerInputDispatcher shouldn't be used " +
                "without RunnerManagerBehavior."
            );
        }
        else
        {
            receivers = new RunnerInputReceiver[rmb.runners.Length];
            for (int i = 0; i < receivers.Length; ++i)
            {
                if (rmb.runners[i] != null)
                {
                    receivers[i] =
                        rmb.runners[i]
                            .GetComponent<RunnerInputReceiver>();
                }
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < receivers.Length; ++i)
        {
            if (receivers[i] != null)
            {
                float x = Input.GetAxis(controls[i][(int) Controls.X]);
                float y = Input.GetAxis(controls[i][(int) Controls.Y]);
                bool jump =
                    Input.GetButtonDown(
                        controls[i][(int) Controls.Jump]);
                bool dash =
                    Input.GetButtonDown(
                        controls[i][(int) Controls.Dash]);
                receivers[i].SetAxes(x, y);
                receivers[i].SetJump(jump);
                receivers[i].SetDash(dash);
            }
        }
        float keyX = 0.0f;
        float keyY = 0.0f;
        if (Input.GetKey("up"))
        {
            keyY += 1.0f;
        }
        if (Input.GetKey("down"))
        {
            keyY -= 1.0f;
        }
        if (Input.GetKey("left"))
        {
            keyX -= 1.0f;
        }
        if (Input.GetKey("right"))
        {
            keyX += 1.0f;
        }
        if (keyX > 0.1 || keyX < -0.1
            || keyY > 0.1 || keyY < -0.1)
        {
            receivers[keyboardIsFor].SetAxes(keyX, keyY);
        }
        if (Input.GetKeyDown("space"))
        {
            receivers[keyboardIsFor].SetJump(true);
        }
    }
}
