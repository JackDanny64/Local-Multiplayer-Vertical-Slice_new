using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public Gun gun;                     // The player's gun
    public PlayerHealth playerHealth;   // The player's health

    [Header("UI Elements")]
    public TMP_Text ammoText;           // Ammo text
    public Image healthFillImage;       // Health bar fill image

    void Start()
    {
        if (gun == null)
            gun = GetComponentInParent<Gun>();
    }

    void Update()
    {
        UpdateAmmoUI();
        UpdateHealthUI();
    }

    void UpdateAmmoUI()
    {
        if (gun != null && ammoText != null)
        {
            ammoText.text = $"{gun.CurrentAmmo} / {gun.MagazineSize}";
        }
    }

    void UpdateHealthUI()
    {
        if (playerHealth != null && healthFillImage != null)
        {
            // Update health bar fill
            float fillAmount = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
            healthFillImage.fillAmount = fillAmount;

            // Optional: color change from green -> red
            healthFillImage.color = Color.Lerp(Color.red, Color.green, fillAmount);
        }
    }
}