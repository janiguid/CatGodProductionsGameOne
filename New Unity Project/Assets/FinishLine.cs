using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D pc) {
    //for the types that slow all players, will need to access all the players somehow
        if (pc.CompareTag("Player")) {
            //need some kinda logic to get to the endgame screen with a victory
            //scenes not built yet tho lol
            //probably want some kinda in between animation that changes scene on animation end
            //anyways use this
            //SceneManager.LoadScene("EndGame");
        }
    }
}
