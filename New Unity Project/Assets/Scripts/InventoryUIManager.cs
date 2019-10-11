using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
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
        if (MyData.RefreshedData == false)
        {
            RefreshUI();
            print("swear to fkin god");

        }
    }

    void RefreshUI()
    {
        //LeftItem.GetComponent<SpriteRenderer>().sprite = MyData.LeftSprite;
        //CenterItem.GetComponent<SpriteRenderer>().sprite = MyData.CenterSprite;
        //RightItem.GetComponent<SpriteRenderer>().sprite = MyData.RightSprite;

        LeftItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        CenterItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[1].gameObject.GetComponent<SpriteRenderer>().sprite;
        RightItem.GetComponent<SpriteRenderer>().sprite = MyData.ItemList[2].gameObject.GetComponent<SpriteRenderer>().sprite;


        MyData.RefreshedData = true;
    }
}
