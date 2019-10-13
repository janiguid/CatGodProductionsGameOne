using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInventory : MonoBehaviour
{
    [SerializeField]
    private int InventorySize = 3;

    public GameObject [] ItemList;
    public InventoryData MyData;


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
        if (Use)
        {
            UseItem();
        }
        else if (Swapping && Right)
        {
            SwitchRight();
        }
        else if (Swapping && Left)
        {
            SwitchLeft();
        }
    }

    //Need to set array spot to null
    public void UseItem()
    {
        //Instantiate game object
    }

    //Takes inventory array and shift its 
    //elements to the right
    public void SwitchRight()
    {
        print("switched to the right");

        GameObject temp = ItemList[2];
        ItemList[2] = ItemList[1];
        ItemList[1] = ItemList[0];
        ItemList[0] = temp;

    }

    //Takes inventory array and shift its 
    //elements to the right
    public void SwitchLeft()
    {
        print("switched to the left");

        GameObject temp = ItemList[0];
        ItemList[0] = ItemList[1];
        ItemList[1] = ItemList[2];
        ItemList[2] = temp;

    }



}
