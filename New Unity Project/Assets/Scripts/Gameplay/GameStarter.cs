using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public List<GameObject> Runners;
    public List<PlayerData> PlayerInfos;

    //This must remain constant
    //0 is cat
    //1 is eye
    //2 is wind
    //3 is water
    public List<Sprite> PlayerIcons;
    public Image[] Placeholder;

    private void Awake()
    {
        for(int i = 0; i < Runners.Count; ++i)
        {
            Runners[i].gameObject.GetComponent<SpriteRenderer>().sprite = PlayerInfos[i].PlayerSprite;
        }


        //doesn't work. will fix later. 
        for(int i = 0; i < PlayerInfos.Count; ++i)
        {
            for(int j = 0; j < Placeholder.Length; ++j)
            {
                if(j == PlayerInfos[i].CharacterNumber)
                {
                    Placeholder[j].sprite = PlayerIcons[j];
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

  
}
