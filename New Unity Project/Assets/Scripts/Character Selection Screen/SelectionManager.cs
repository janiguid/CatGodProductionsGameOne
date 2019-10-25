using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int ReadyPlayers = 0;
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

            if (playerInfos[j].GetPlayerNumber() == -5 || playerInfos[j].Selected()) continue;

            //switch border to right
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


            //switch border to left
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

            //set it so the joystick needs to return to default value
            //before user can go left or right again
            if (Input.GetAxis(controls[j][0]) == 0)
            {
                playerInfos[j].SetMove(false);
            }

            //player has chosen the character
            if (Input.GetButtonDown(controls[j][3]) && playerInfos[j].GetPlayerNumber() != -5 && playerInfos[j].Selected() == false)
            {
                print("attached the sprite");
                ++ReadyPlayers;
                PlayerDatas[j].PlayerSprite = SpriteList[playerInfos[j].Index];
                playerInfos[j].SetSelected(true);
                PlayerDatas[j].CharacterNumber = playerInfos[j].Index;
            }

            //if all players are ready, load next scene
            if(ReadyPlayers == 3)
            {
                SceneManager.LoadScene(2);
            }

        }
        for (int i = 0; i < controls.Length; ++i)
        {
            if (Input.GetButtonDown(controls[i][3]) && playerInfos[i].GetPlayerNumber() == -5)
            {
                playerInfos[i].SetPlayerNumber(i);
                playerInfos[i].ActivateBorder();
            }
        }

    }

}
