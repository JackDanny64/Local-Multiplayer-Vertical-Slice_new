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

    [Header("Stamina UI")]
    public FPSControllerRigidbody playerController; // assign the player controller
    public Image staminaFillImage; // stamina bar fill

    void Start()
    {
        if (gun == null)
            gun = GetComponentInParent<Gun>();
    }

    void Update()
    {
        UpdateAmmoUI();
        UpdateHealthUI();
        UpdateStaminaUI();
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
    void UpdateStaminaUI()
    {
        if (playerController != null && staminaFillImage != null)
        {
            float fillAmount = playerController.currentStamina / playerController.maxStamina;
            staminaFillImage.fillAmount = fillAmount;

            // Optional: color change (green -> yellow -> red)
            staminaFillImage.color = Color.Lerp(Color.red, Color.blue, fillAmount);
        }
    }
}