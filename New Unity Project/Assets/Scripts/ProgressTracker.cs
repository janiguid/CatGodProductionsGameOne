﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    [SerializeField]
    private Slider ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ProgressBar.value < ProgressBar.maxValue)
        {
            ProgressBar.value += Time.deltaTime;
        }
        
    }
}
