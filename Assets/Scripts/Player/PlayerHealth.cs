using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public int playerID;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Accept the shooterID
    public void TakeDamage(int damage, int shooterID)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(shooterID);
        }
    }

    void Die(int killerID)
    {
        Debug.Log("Player " + playerID + " died");

        // Give point to the actual killer
        GameManager.instance.PlayerDied(killerID, this);

        gameObject.SetActive(false);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}