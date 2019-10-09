using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private int InventorySize = 3;

    public GameObject [] ItemList;
    GameObject CurrentItem;

    public string[] testArray;
    public InventoryData MyData;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchRight();
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchLeft();
        }
    }

    void UseItem()
    {
        print("used item!");
    }

    void SwitchRight()
    {
        print("switched to the right");

        GameObject temp = ItemList[2];
        ItemList[2] = ItemList[1];
        ItemList[1] = ItemList[0];
        ItemList[0] = temp;


        RefreshData();
    }

    void SwitchLeft()
    {
        print("switched to the left");

        GameObject temp = ItemList[0];
        ItemList[0] = ItemList[1];
        ItemList[1] = ItemList[2];
        ItemList[2] = temp;


        RefreshData();
    }


    void RefreshData()
    {
        //Maybe get the spriterenderers at the start so theres no need to keep getting each component
        MyData.LeftSprite = ItemList[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        MyData.CenterSprite = ItemList[1].gameObject.GetComponent<SpriteRenderer>().sprite;
        MyData.RightSprite = ItemList[2].gameObject.GetComponent<SpriteRenderer>().sprite;

    }

}
