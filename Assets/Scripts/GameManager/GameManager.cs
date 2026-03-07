using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int player1Score;
    public int player2Score;

    public float roundTime = 120f;

    public Transform player1Spawn;
    public Transform player2Spawn;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        roundTime -= Time.deltaTime;

        if (roundTime <= 0)
        {
            EndRound();
        }
    }

    public void PlayerDied(int playerID, PlayerHealth player)
    {
        // Increase score for the other player
        if (playerID == 1)
            player2Score++;
        else
            player1Score++;

        Debug.Log("Score P1: " + player1Score + " P2: " + player2Score);

        // Respawn the player after 1 second
        StartCoroutine(RespawnPlayer(playerID, player));
    }

    System.Collections.IEnumerator RespawnPlayer(int playerID, PlayerHealth player)
    {
        yield return new WaitForSeconds(1f); // wait a bit before respawn

        // Reset health
        player.ResetHealth();

        // Move to spawn point
        if (playerID == 1)
            player.transform.position = player1Spawn.position;
        else
            player.transform.position = player2Spawn.position;

        // Reactivate player
        player.gameObject.SetActive(true);
    }

    void EndRound()
    {
        Debug.Log("Round Over");

        if (player1Score > player2Score)
            Debug.Log("Player 1 Wins");
        else if (player2Score > player1Score)
            Debug.Log("Player 2 Wins");
        else
            Debug.Log("Draw");

        Time.timeScale = 0;
    }
}