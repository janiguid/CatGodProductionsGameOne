using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public List<GameObject> Runners;
    public List<PlayerData> PlayerInfos;

    private void Awake()
    {
        for(int i = 0; i < Runners.Count; ++i)
        {
            Runners[i].gameObject.GetComponent<SpriteRenderer>().sprite = PlayerInfos[i].PlayerSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

  
}
