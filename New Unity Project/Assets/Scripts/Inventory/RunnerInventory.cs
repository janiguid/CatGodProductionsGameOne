﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInventory : MonoBehaviour
{
    [SerializeField]
    private int InventorySize = 3;

    public List<GameObject> ItemList;
    public InventoryData MyData;

    public Transform PlayerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        ItemList = MyData.ItemList;
    }

    // Called in InventoryManager
    // and uses data passed onto it to either:
    // Use an item, switch right, or switch left
    public void UpdateInput(bool Swapping, bool Left, bool Right, bool Use)
    {
        //I'm using item count because I can't check if a list is null
        if (MyData.ItemCount == 0) return;
        //if (Use)
        //{
        //    UseItem();
        //}
        //else if (Swapping && Right)
        //{
        //    SwitchRight();
        //}
        //else if (Swapping && Left)
        //{
        //    SwitchLeft();
        //}

        //had to swap because of UI
        if (Use)
        {
            UseItem();
        }
        else if (Swapping && Right)
        {
            
            SwitchLeft();
        }
        else if (Swapping && Left)
        {
            SwitchRight();
        }
    }

    //Need to set array spot to null
    public void UseItem()
    {
        //Instantiate(ItemList[MyData.ItemCount - 1], PlayerSpawnPoint.transform);

        //Instantiate game object
        ItemList.RemoveAt(MyData.ItemCount - 1);

        

        MyData.ItemCount--;

        MyData.refreshed = false;
    }

    //Takes inventory array and shift its 
    //elements to the right
    public void SwitchRight()
    {
        
        GameObject temp = ItemList[ItemList.Count - 1];

        for(int i = ItemList.Count - 1; i != 0; --i)
        {
            ItemList[i] = ItemList[i - 1];
        }

        ItemList[0] = temp;

        MyData.refreshed = false;
    }

    //Takes inventory array and shift its 
    //elements to the right
    public void SwitchLeft()
    {
        GameObject temp = ItemList[0];

        for (int i = 0; i < ItemList.Count - 1; ++i)
        {
            ItemList[i] = ItemList[i + 1];
        }

        ItemList[ItemList.Count - 1] = temp;

        MyData.refreshed = false;
    }



}