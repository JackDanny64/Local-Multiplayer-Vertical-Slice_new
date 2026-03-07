using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSpawner : MonoBehaviour
{
    public Transform[] Spawnpoints;
    private int playercount;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.position = Spawnpoints[playercount].transform.position;
        if(playercount == 0)
        {
            playerInput.GetComponent<FPSControllerRigidbody>();
        }
        playercount++;
    }
}