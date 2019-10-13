using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalItemData : ScriptableObject
{
    // Jessie, hope you don't mind I've provisionally added
    // some inventory-related stuff I needed to implement Pickup.
    // Please feel free to mess around with it as you see fit,
    // reimplement it by your preferred means, etc.
    // - Jaime
    public GameObject[] ItemUIPrefabs;
    public string[] ItemNames;
    public delegate void ItemEffect(int runnerID);
    public ItemEffect[] ItemEffects;
    private Dictionary<string, int> ItemLookupCache;
    void Start()
    {
        for (int i = 0; i < ItemNames.Length; ++i)
        {
            ItemLookupCache[ItemNames[i]] = i;
        }
    }
    public GameObject MakeItemUIObject(string what)
    {
        GameObject ret = (GameObject) Instantiate(
            ItemUIPrefabs[ItemLookupCache[what]],
            Vector3.zero, Quaternion.identity
        );
        ret.GetComponent<Item>().ItemName = what;
        return ret;
    }
    public void DoItemEffect(string what, int runnerID)
    {
        ItemEffects[ItemLookupCache[what]](runnerID);
    }
    public void DoItemEffect(GameObject what, int runnerID)
    {
        DoItemEffect(what.GetComponent<Item>().ItemName, runnerID);
    }
}
