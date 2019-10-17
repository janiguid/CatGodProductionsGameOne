using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BeginPlay()
    {
        SceneManager.LoadScene("Character Selection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
