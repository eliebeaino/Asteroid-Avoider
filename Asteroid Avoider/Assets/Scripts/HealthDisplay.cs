using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    
    [SerializeField] Image healthy;
    [SerializeField] Image hurt;
    [SerializeField] GridLayoutGroup hpBox;
    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] private List<Image> health = new List<Image>();

    int initialHealth = 0;

    private void Start()
    {
        initialHealth = playerHealth.GetPlayerHP();
        SpawnHPBar();
    }

    // Use events to update health instead of Update method
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHP;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHP;
    }

    // this spawns HP hearts UI according to the given player health class
    private void SpawnHPBar()
    {
        for (int i = 0; i < initialHealth; i++)
        {
            Image newHPBlock = Instantiate(healthy, hpBox.transform);
            health.Add(newHPBlock);
        }
    }

    // Gets called if health changes
    private void UpdateHP()
    {
        int currentHealth = playerHealth.GetPlayerHP();

        // Loop through all health blocks and update their sprites
        for (int i = 0; i < health.Count; i++)
        {
            if (i < currentHealth)
            {
                // Use the healthy sprite if the player's health is greater than the index
                health[i].sprite = healthy.sprite;
            }
            else
            {
                // Otherwise, use the hurt sprite for the remaining health blocks
                health[i].sprite = hurt.sprite;
            }
        }
    }
}
