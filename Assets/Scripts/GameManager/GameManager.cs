using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int player1Score;
    public int player2Score;

    public float roundTime = 120f;

    public Transform player1Spawn;
    public Transform player2Spawn;

    private bool gameStarted = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!gameStarted)
            return;

        roundTime -= Time.deltaTime;

        if (roundTime <= 0)
        {
            EndRound();
        }
    }

    public void StartRound()
    {
        Debug.Log("Both players joined. Starting round!");
        gameStarted = true;
    }

    // This is the new method for handling death
    public void KillPlayer(PlayerHealth player)
    {
        // Increment opponent's score
        if (player.playerID == 1)
            player2Score++;
        else
            player1Score++;

        // Optional delay for death animation
        StartCoroutine(RespawnPlayer(player));
    }

    // Coroutine to handle respawn after short delay
    private IEnumerator RespawnPlayer(PlayerHealth player)
    {
        yield return new WaitForSeconds(1f); // wait for 1 second before respawn

        // Reset health
        player.ResetHealth();

        // Move to spawn point
        if (player.playerID == 1)
            player.transform.position = player1Spawn.position;
        else
            player.transform.position = player2Spawn.position;

        // Reset Rigidbody and controller state
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        FPSControllerRigidbody controller = player.GetComponent<FPSControllerRigidbody>();
        controller.ResetPlayerState(); // You need to add this method in FPSControllerRigidbody
    }

    void EndRound()
    {
        Debug.Log("Round Over");

        Time.timeScale = 0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
    }
}