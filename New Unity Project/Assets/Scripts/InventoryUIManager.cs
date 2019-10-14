using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    //Could possibly turn this into a static class or a singleton 
    //so we don't have to use Update()

    //Instantiate this in editor
    [SerializeField]
    private List<GameObject> Items;

    public InventoryData MyData;
    public GameObject empty;

    public float InventorySize;
    public float spacing;

    public SpriteRenderer Highlight;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            Items[i].GetComponent<SpriteRenderer>().sprite = empty.gameObject.GetComponent<SpriteRenderer>().sprite;

            float finalX = gameObject.transform.localPosition.x + (InventorySize / 3) + spacing * i;

            Items[i].transform.localPosition = new Vector2(finalX - ((InventorySize / 3) + spacing), gameObject.transform.localPosition.y);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(MyData.refreshed == false)
        {
            RefreshUI();
        }
        for (int i = 0; i < 3; ++i)
        {

            print("final pos: " + Items[i].transform.position);
        }

    }

    //Checks the recently changed inventory data
    //and updates the canvas (UI)
    void RefreshUI()
    {
        for (int i = 0; i < Items.Count; ++i)
        {
            Items[i].GetComponent<SpriteRenderer>().sprite = empty.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = 0; i < MyData.ItemCount; ++i)
        {
            if (Items[i].activeSelf == false) continue;
            Items[i].GetComponent<SpriteRenderer>().sprite = MyData.ItemList[i].gameObject.GetComponent<SpriteRenderer>().sprite;
        }

        Highlight.gameObject.transform.position = new Vector3(Items[MyData.ItemCount - 1].transform.position.x, Items[Items.Count - 1].transform.position.y, 1);

        Recenter();

        MyData.refreshed = true;
    }

    //Recenter objects when an object is used
    void Recenter()
    {
        float CenterAdjustment = 0;
        if (MyData.ItemCount % 2 == 0 )
        {
            CenterAdjustment = ((InventorySize / 3) + spacing) / 2;
        }
        else
        {
            CenterAdjustment = ((InventorySize / 3) + spacing);
        }
             
        for (int i = 0; i < MyData.ItemCount; ++i)
        {

            float finalX = gameObject.transform.localPosition.x + (InventorySize / 3) + spacing * i;

            if (MyData.ItemCount == 1) finalX = CenterAdjustment = 0;
            Items[i].transform.localPosition = new Vector2(finalX - CenterAdjustment, gameObject.transform.localPosition.y);
        }

        if (MyData.ItemCount == 0) return;
        Highlight.gameObject.transform.position = new Vector3(Items[MyData.ItemCount - 1].transform.position.x, Items[Items.Count - 1].transform.position.y, 1);
    }

}

