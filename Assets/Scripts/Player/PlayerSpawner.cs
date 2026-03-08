using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] Spawnpoints;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public Camera lobbyCamera; // camera used before players join

    private int playerCount = 0;
    private PlayerInputManager manager;

    void Start()
    {
        manager = PlayerInputManager.instance;

        // First player prefab
        manager.playerPrefab = Player1Prefab;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerCount >= Spawnpoints.Length)
        {
            Debug.LogWarning("PlayerCount exceeds spawn points.");
            return;
        }

        // Disable lobby camera when first player joins
        if (playerCount == 0 && lobbyCamera != null)
        {
            lobbyCamera.gameObject.SetActive(false);
        }

        // Move player to spawn point
        playerInput.transform.position = Spawnpoints[playerCount].position;

        playerCount++;

        if (playerCount == 1)
        {
            manager.playerPrefab = Player2Prefab;
        }

        // Start game when both players join
        if (playerCount == 2)
        {
            GameManager.instance.StartRound();
        }
    }
}