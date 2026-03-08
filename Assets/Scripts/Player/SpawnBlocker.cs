using UnityEngine;

public class SpawnBlocker : MonoBehaviour
{
    // Tag of the player that is blocked from this spawn
    public string blockedTag = "Player1";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(blockedTag))
        {
            // Stop movement
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                // Optional: push back slightly
                rb.MovePosition(other.transform.position - other.transform.forward * 1f);
            }
        }
    }
}