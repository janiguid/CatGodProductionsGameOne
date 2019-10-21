using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject
{
    //Contains inventory data that other 
    //inventory classes can quickly reference
    public List<GameObject> ItemList;


    public int ItemCount;

    public bool refreshed;

    public void AddItem(GameObject item)
    {
        if (ItemList.Count > 3) return;

        ItemList.Add(item);
        refreshed = false;
    }

}
