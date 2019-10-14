using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject, ISerializationCallbackReceiver
{
    //Contains inventory data that other 
    //inventory classes can quickly reference
    public List<GameObject> ItemList;

    public List<GameObject> InitialValues;

    public int ItemCount;

    public bool refreshed;

    public void OnAfterDeserialize()
    {
        refreshed = false;
        for(int i = 0; i < InitialValues.Count; ++i)
        {
            ItemList[i] = InitialValues[i];
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
