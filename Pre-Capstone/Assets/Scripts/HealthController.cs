using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private float playerHealth;
    public PlayerController player;
    private float maxHealth = 20;
    [SerializeField] private Image healthImage;

    private void Update()
    {
        playerHealth = player.health;
        UpdateHealth();

    }

    private void UpdateHealth()
    {
        healthImage.fillAmount = playerHealth / maxHealth;
    }
}
