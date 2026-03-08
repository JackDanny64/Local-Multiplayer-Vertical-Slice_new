using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    public TMP_Text winnerText;
    public Button retryButton;
    public Button menuButton;

    void Start()
    {
        // Set winner text based on GameManager scores
        int p1 = GameManager.instance.player1Score;
        int p2 = GameManager.instance.player2Score;

        if (p1 > p2)
            winnerText.text = "Player 1 Wins!";
        else if (p2 > p1)
            winnerText.text = "Player 2 Wins!";
        else
            winnerText.text = "Draw!";

        // Assign button actions
        retryButton.onClick.AddListener(RetryGame);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene("MainLevel"); // Replace with your main game scene name
    }

   public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}