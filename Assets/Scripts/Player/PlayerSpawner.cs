using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] Spawnpoints;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    private int playerCount = 0;
    private PlayerInputManager manager;

    void Start()
    {
        manager = PlayerInputManager.instance;

        // First player will spawn this prefab
        manager.playerPrefab = Player1Prefab;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerCount >= Spawnpoints.Length)
        {
            Debug.LogWarning("PlayerCount exceeds spawn points. Ignoring join.");
            return;
        }

        // Move player to correct spawn point
        playerInput.transform.position = Spawnpoints[playerCount].position;

        playerCount++;

        // Change prefab for the next player
        if (playerCount == 1)
        {
            manager.playerPrefab = Player2Prefab;
        }
    }
}