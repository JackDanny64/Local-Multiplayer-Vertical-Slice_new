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

    [Header("Game Timer & Score UI")]
    public TMP_Text timerText;          // Timer countdown
    public TMP_Text player1ScoreText;   // Player 1 score
    public TMP_Text player2ScoreText;   // Player 2 score

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
        UpdateTimerUI();
        UpdateScoreUI();
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
    void UpdateTimerUI()
    {
        if (timerText != null && GameManager.instance != null)
        {
            float time = Mathf.Max(GameManager.instance.roundTime, 0);
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void UpdateScoreUI()
    {
        if (GameManager.instance != null)
        {
            if (player1ScoreText != null)
                player1ScoreText.text = GameManager.instance.player1Score.ToString();

            if (player2ScoreText != null)
                player2ScoreText.text = GameManager.instance.player2Score.ToString();
        }
    }
}