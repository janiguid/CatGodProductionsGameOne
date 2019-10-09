using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer LeftItem;
    [SerializeField]
    private SpriteRenderer CenterItem;
    [SerializeField]
    private SpriteRenderer RightItem;


    public InventoryData MyData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LeftItem.sprite = MyData.LeftSprite;
        CenterItem.sprite = MyData.CenterSprite;
        RightItem.sprite = MyData.RightSprite;
    }
}
