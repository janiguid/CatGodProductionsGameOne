using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    [SerializeField]
    private Slider ProgressBar;
    public ObstacleController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ProgressBar.value < ProgressBar.maxValue)
        {
            //ProgressBar.value += controller.GetTime();
            Mathf.SmoothStep(ProgressBar.value, ProgressBar.value += controller.GetTime()*3.25f, 0f);
        }
        
    }
}
