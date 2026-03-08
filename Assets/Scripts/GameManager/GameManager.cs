using UnityEngine;

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

    public void PlayerDied(int playerID, PlayerHealth player)
    {
        if (playerID == 1)
            player2Score++;
        else
            player1Score++;

        StartCoroutine(RespawnPlayer(playerID, player));
    }

    System.Collections.IEnumerator RespawnPlayer(int playerID, PlayerHealth player)
    {
        yield return new WaitForSeconds(1f);

        player.ResetHealth();

        if (playerID == 1)
            player.transform.position = player1Spawn.position;
        else
            player.transform.position = player2Spawn.position;

        player.gameObject.SetActive(true);
    }

    void EndRound()
    {
        Debug.Log("Round Over");

        Time.timeScale = 0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
    }
}