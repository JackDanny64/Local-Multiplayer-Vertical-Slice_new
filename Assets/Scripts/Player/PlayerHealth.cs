using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public int playerID; // 1 or 2
    private bool isDead = false; // NEW: track if player is dead

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    // Accept the shooterID (optional)
    public void TakeDamage(int damage, int shooterID)
    {
        if (isDead) return; // ignore damage if already dead

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // just in case
        isDead = true;

        Debug.Log("Player " + playerID + " died");

        // Call the new KillPlayer method in GameManager
        GameManager.instance.KillPlayer(this);

        // Don't deactivate the GameObject, respawn logic will move and reset it
        // gameObject.SetActive(false); // REMOVE THIS LINE
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false; // reset dead state so player can die again
    }
}