using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject, ISerializationCallbackReceiver
{
    //Contains inventory data that other 
    //inventory classes can quickly reference
    public List<GameObject> ItemList;


    public int ItemCount;

    public bool refreshed;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }
}
