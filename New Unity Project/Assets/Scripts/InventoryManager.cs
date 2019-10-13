using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /*
     * Controls as of right now:
     * P1: shift is q, left is o and right is p
     * P2: shift is a, left is k and right is l
     * P3: shift is z, left is n and right is m
     */

    public RunnerInventory[] Inventories;

    public GlobalItemData itemData;

    private static readonly string[][] controls =
    {
        new string[]
        {
            "P1 Shift",
            "P1 Switch Left",
            "P1 Switch Right",
            "P1 Use Item"
        },
        new string[]
        {
            "P2 Shift",
            "P2 Switch Left",
            "P2 Switch Right",
            "P2 Use Item"
        },
        new string[]
        {
            "P3 Shift",
            "P3 Switch Left",
            "P3 Switch Right",
            "P3 Use Item"
        },
        new string[]
        {
            "P4 Shift",
            "P4 Switch Left",
            "P4 Switch Right",
            "P4 Use Item"
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Goes through an array of RunnerInventory 
    // and goes through each one, checking each 
    // player's input
    void Update()
    {
        for(int i = 0; i < Inventories.Length; ++i)
        {
            Inventories[i].UpdateInput(
                Input.GetButton(controls[i][0]),
                Input.GetButtonDown(controls[i][1]),
                Input.GetButtonDown(controls[i][2]),
                Input.GetButtonDown(controls[i][3]));
        }
    }
}
