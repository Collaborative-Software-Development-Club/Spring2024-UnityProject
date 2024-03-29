using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Replace "MainMenu" with the name of your main scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

