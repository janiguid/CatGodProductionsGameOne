using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    //Will change the sprite of the player
    //in the next scene depending on which character 
    //they choose
    public List<PlayerData> PlayerDatas;
    public List<Sprite> SpriteList;
    //border game objects and index
    public List<GameObject> Borders;
    //constant positions
    public List<RectTransform> Positions;

    public List<PlayerInfo> playerInfos;

    private static readonly string[][] controls =
{
        new string[]
        {
            "P1 Horizontal",
            "P1 Vertical",
            "P1 Jump",
            "P1 Dash"
        },
        new string[]
        {
            "P2 Horizontal",
            "P2 Vertical",
            "P2 Jump",
            "P2 Dash"
        },
        new string[]
        {
            "P3 Horizontal",
            "P3 Vertical",
            "P3 Jump",
            "P3 Dash"
        },
        new string[]
        {
            "P4 Horizontal",
            "P4 Vertical",
            "P4 Jump",
            "P4 Dash"
        }
    };

    public class PlayerInfo
    {
        public GameObject Border;
        public int Index;
        public int PlayerNumber;
        public bool CanMove;
        public bool SelectedCharacter;
        public PlayerInfo(GameObject bor, int ind, int pNum, bool mobile, bool chose)
        {
            Border = bor;
            Index = ind;
            PlayerNumber = pNum;
            CanMove = mobile;
            SelectedCharacter = chose;
        }

        public bool GetMove()
        {
            return CanMove;
        }

        public void SetMove(bool value)
        {
            CanMove = value;
        }

        public void SetIndex(int num)
        {
            Index = num;
            print("New Index is: " + Index);
        }

        public void SetPlayerNumber(int pID)
        {
            PlayerNumber = pID;
            print("New Player Number is: " + PlayerNumber);
        }

        public int GetPlayerNumber()
        {
            return PlayerNumber;
        }

        public void ActivateBorder()
        {
            Border.SetActive(true);
        }

        public void MoveRight(Vector3 next)
        {
            Border.GetComponent<RectTransform>().position = next;
        }

        public void MoveLeft(Vector3 next)
        {
            Border.GetComponent<RectTransform>().position = next;
        }

        public bool Selected()
        {
            return SelectedCharacter;
        }

        public void SetSelected(bool select)
        {
            SelectedCharacter = select;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInfos = new List<PlayerInfo>();

        //instantiate all PlayerInfo structs and just assign one 
        //to each player when they press X
        for(int i = 0; i < 4; ++i)
        {
            playerInfos.Add(new PlayerInfo(Borders[i], i, -5, false, false));
        }
    }


    // Update is called once per frame
    void Update()
    {




        for(int j = 0; j< controls.Length; ++j)
        {

            //print("player " + j + " and player num is: " + playerInfos[j].GetPlayerNumber() );
            if (playerInfos[j].GetPlayerNumber() == -5 || playerInfos[j].Selected()) continue;
           //print("no ones getting through this");
            if (Input.GetAxis(controls[j][0]) > 0)
            {
                if(playerInfos[j].GetMove() == false && playerInfos[j].Index < 3)
                {
                    playerInfos[j].SetIndex(playerInfos[j].Index + 1);
                    playerInfos[j].MoveRight(Positions[playerInfos[j].Index].transform.position);
                    //we set it to true so we don't call this multiple times
                    playerInfos[j].SetMove(true);
                }

            }

            if (Input.GetAxis(controls[j][0]) < 0)
            {
                print(playerInfos[j].Index);
                if (playerInfos[j].GetMove() == false && playerInfos[j].Index > 0)
                {
                    playerInfos[j].SetIndex(playerInfos[j].Index - 1);
                    playerInfos[j].MoveLeft(Positions[playerInfos[j].Index].transform.position);
                    //we set it to true so we don't call this multiple times
                    playerInfos[j].SetMove(true);
                }

            }

            if (Input.GetAxis(controls[j][0]) == 0)
            {
                playerInfos[j].SetMove(false);
            }

            if (Input.GetButtonDown(controls[j][3]) && playerInfos[j].GetPlayerNumber() != -5 && playerInfos[j].Selected() == false)
            {
                print("attached the sprite");
                PlayerDatas[j].PlayerSprite = SpriteList[playerInfos[j].Index];
                playerInfos[j].SetSelected(true);
            }

        }
        for (int i = 0; i < controls.Length; ++i)
        {
            if (Input.GetButtonDown(controls[i][3]) && playerInfos[i].GetPlayerNumber() == -5)
            {
                playerInfos[i].SetPlayerNumber(i);
                //print("new player num is: " + playerInfos[i].GetPlayerNumber());
                playerInfos[i].ActivateBorder();
            }
        }

    }

}
