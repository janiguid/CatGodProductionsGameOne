using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    //Could possibly turn this into a static class or a singleton 
    //so we don't have to use Update()
    [SerializeField]
    private GameObject LeftItem;
    [SerializeField]
    private GameObject CenterItem;
    [SerializeField]
    private GameObject RightItem;



    public InventoryData MyData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RefreshUI();

    }

    //Checks the recently changed inventory data
    //and updates the canvas (UI)
    void RefreshUI()
    {
        LeftItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        CenterItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[1].gameObject.GetComponent<SpriteRenderer>().sprite;
        RightItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[2].gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
