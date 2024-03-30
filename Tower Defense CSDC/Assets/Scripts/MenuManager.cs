using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private bool paused;
    private bool inGame;
    [SerializeField] private Slider volume;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject menu;
    [SerializeField] private String sceneName;

    // General Management

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        inGame = IsInGame();
        
        //When the main scene is initialized the menu should start off disabled
        if (inGame)
        {
            menu.SetActive(false);
            settings.SetActive(false);
        }
    }

    // Update is called once per frame
    /*
     * Checks for if the escape button is pressed while the user is in the main scene
     * if so it either pauses or resumes the game depending on the game's current state
     */
    void Update()
    {
        //If in the main scene, the user should be able to pause the game by pressing escape
        if (inGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            {
                Pause();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            {
                Resume();
            }
        }
    }

    /*
     * Determines then returns if the user is in the main scene based off which scene the menu can direct them to
     */
    private bool IsInGame()
    {
        return sceneName.Equals("Main Menu");
    }


    //Pause Management

    /*
     * Enable the initial pause menu, pause the game, and unlock the cursor
     */
    public void Pause()
    {
        paused = true;
        menu.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;

    }

    /*
     * Disables all menus, resumes time, then locks the cursor to the center of the screen
     */
    public void Resume()
    {
        paused = false;
        menu.SetActive(false);
        settings.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }


    //Scene Management
    /*
     * Moves to a scene determined by sceneName
     */
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    /*
     * Exits the game
     */
    public void Quit()
    {
        Application.Quit();
    }


    //Volume Management

    /*
     * When the settings menu is exited, saves the volume selected
     */
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", volume.value);
    }

    /*
     * When the settings menu is entered, loads the volume selected
     */
    private void OnEnable()
    {
        volume.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volume.value;
    }

    /*
     * Changes the volume of the game based off the slider object
     */
    public void SetVolume()
    {

        AudioListener.volume = volume.value;

    }

    /*
     * Transitions between settings and the menu before it and vice versa
     */
    public void ToggleSettings()
    {
        menu.SetActive(!menu.activeSelf);
        settings.SetActive(!settings.activeSelf);
    }
}
