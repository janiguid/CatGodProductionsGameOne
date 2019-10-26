using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    public Slider MyHealthUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateHealthUI(int health)
    {
        MyHealthUI.value = health;
    }
}
