using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryResetter : MonoBehaviour
{
    public InventoryData[] inventoryDatas;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i= 0; i < inventoryDatas.Length; ++i)
        {
            inventoryDatas[i].ItemList.Clear();
            inventoryDatas[i].refreshed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
