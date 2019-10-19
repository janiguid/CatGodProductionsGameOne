using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{

    public Image[] MyImages;
    public InventoryData MyData;


    // Update is called once per frame
    void Update()
    {
        if(MyData.refreshed == false)
        {
            RefreshUI();
        }


    }

    //Checks the recently changed inventory data
    //and updates the canvas (UI)
    void RefreshUI()
    {
        //clear images and prep for new sprites
        for (int i = 0; i < MyImages.Length; ++i)
        {
            MyImages[i].sprite = null;
        }


        //don't do anything when no items
        if(MyData.ItemList.Count == 0)
        {
            return;
        }

        //since MyImages and MyData will have differing
        //lengths once the player uses an item, we're using
        //remainingItemCount to act as the index for MyData.ItemList
        //so we don't get any array out of bounds exceptions
        int remainingItemCount = MyData.ItemList.Count - 1;
        for (int i = 2; i > -1; --i)
        {
            print("Changing sprites");
            if (remainingItemCount == -1) break;
            MyImages[i].sprite = MyData.ItemList[remainingItemCount].GetComponent<SpriteRenderer>().sprite;
            --remainingItemCount;
        }
        print("Changed sprites");

        MyData.refreshed = true;
    }



}

