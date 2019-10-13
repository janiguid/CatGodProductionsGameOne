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
            //Items[i].transform.position = new Vector2(gameObject.transform.position.y + (InventorySize / 3) + spacing*i, gameObject.transform.position.y);
        }



    }

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

        MyData.refreshed = true;
    }

    //Recenter objects when an object is used
    void Recenter()
    {

    }

}

