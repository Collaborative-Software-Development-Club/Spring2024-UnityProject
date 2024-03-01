using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayNowButton : MonoBehaviour
{


    public void LoadGameScene()
    {
        Debug.Log("Button clicked! Loading game scene...");

        SceneManager.LoadScene("Main Scene");
    }

}
