using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject
{
    //Contains inventory data that other 
    //inventory classes can quickly reference
    public GameObject[] ItemList;
}
