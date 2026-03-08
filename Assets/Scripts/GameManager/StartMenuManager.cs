using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Replace "GameScene" with your actual gameplay scene name
        SceneManager.LoadScene("MainLevel");
    }

    public void ExitGame()
    {
        // Exit the application
        Debug.Log("Quit Game");
        Application.Quit();
    }
}