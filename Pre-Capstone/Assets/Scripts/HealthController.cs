using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    The HealthController script is used to update the UI's 
    current health shown to match the Players health
 */
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

    //Method to get the image of green/red on the UI
    private void UpdateHealth()
    {
        healthImage.fillAmount = playerHealth / maxHealth;
    }
}
