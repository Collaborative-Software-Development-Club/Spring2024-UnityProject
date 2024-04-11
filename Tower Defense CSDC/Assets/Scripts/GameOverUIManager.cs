using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        MineableResource.metalResource = 0;
        MineableResource.woodResource = 0;
        
        SceneManager.LoadScene("Main Menu"); // Replace "MainMenu" with the name of your main scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

